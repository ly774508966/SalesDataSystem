using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class DataReceiver
{
    public bool IsListening = false;
    public TcpListener listener;


    public Socket socket;
    public byte[] RecBuffer;
    public int BufferSize = 0X4FFFF;
    private int readSize = 10240;
    private int bufferOffset = 0;
    private int msgLength = 0;

    private Queue<byte[]> msgQueue = new Queue<byte[]>();

    private System.Action<byte[]> SyncDataCallBack;

    public DataReceiver(System.Action<byte[]> processCallBack)
    {
        SyncDataCallBack = processCallBack;
    }

    public void update()
    {
        while (msgQueue.Count > 0)
        {
            byte[] data = msgQueue.Dequeue();
            ProcessReceivedData(data);
        }
    }

    public void ProcessReceivedData(byte[] data)
    {
        if (SyncDataCallBack != null)
        {
            SyncDataCallBack(data);
        }
    }

    public void StartListen(int port)
    {
        if (IsListening) Dispose();

        IPAddress localIp = Utility.GetLocalIpv4();
        IPEndPoint endPoint = new IPEndPoint(localIp, port);
        listener = new TcpListener(endPoint);
        listener.Start();

        AsyncCallback callback = new AsyncCallback(AcceptCallBack);
        listener.BeginAcceptSocket(callback, listener);
        IsListening = true;
    }

    public void ClosedListen()
    {
        Dispose();
    }

    public void AcceptCallBack(IAsyncResult ar)
    {
        try
        {
            Socket handle = listener.EndAcceptSocket(ar);
            if (handle == null) return;
            GameEvents.TriggerCheckEvent(eGameEventTypes.DataReceiverConnected);
            RecBuffer = null;
            RecBuffer = new byte[BufferSize];
            bufferOffset = 0;
            msgLength = 0;
            handle.BeginReceive(RecBuffer, 0, readSize, 0, new AsyncCallback(ReceiveCallBack), handle);
        }
        catch
        {
            IsListening = false;
        }
    }

    public void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            Socket handle = (Socket)(ar.AsyncState);
            int length = handle.EndReceive(ar);
            if (length < 1)
            {
                GameEvents.TriggerCheckEvent(eGameEventTypes.DataReceiverDisConnected);
                return;
            }
            ReadBuffer(length);
            handle.BeginReceive(RecBuffer, bufferOffset + msgLength, readSize, 0, new AsyncCallback(ReceiveCallBack), handle);
        }
        catch
        {

        }
    }

    public void ReadBuffer(int length)
    {
        int tempBufferOffset = bufferOffset;
        msgLength += length;
        while (true)
        {
            if (msgLength <= 0)
                break;
            if (bufferOffset + msgLength - tempBufferOffset >= 4)
            {
                int dataLength = BitConverter.ToInt32(RecBuffer, tempBufferOffset);
                tempBufferOffset += 4;
                if (bufferOffset + msgLength - tempBufferOffset >= dataLength)
                {
                    byte[] finalData = new byte[dataLength];
                    Buffer.BlockCopy(RecBuffer, tempBufferOffset, finalData, 0, dataLength);
                    msgQueue.Enqueue(finalData);
                    tempBufferOffset += dataLength;
                    bufferOffset = tempBufferOffset;
                    msgLength -= (dataLength + 4);
                    if (BufferSize - bufferOffset - msgLength < readSize)
                    {
                        Buffer.BlockCopy(RecBuffer, bufferOffset, RecBuffer, 0, msgLength);
                        break;
                    }
                }
                else
                {
                    if (BufferSize - bufferOffset - msgLength < readSize)
                    {
                        Buffer.BlockCopy(RecBuffer, bufferOffset, RecBuffer, 0, msgLength);
                        bufferOffset = 0;
                    }
                    break;
                }
            }
            else
            {
                if (BufferSize - bufferOffset - msgLength < readSize)
                {
                    Buffer.BlockCopy(RecBuffer, bufferOffset, RecBuffer, 0, msgLength);
                    bufferOffset = 0;
                }
                break;
            }
        }

    }

    public void Dispose()
    {
        if (listener != null)
            listener.Stop();
        listener = null;
        IsListening = false;
    }
}
