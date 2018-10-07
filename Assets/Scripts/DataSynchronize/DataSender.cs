using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class DataSender
{
    Socket socket = null;
    public bool IsConnected = false;
    private Queue<byte[]> msgQueue = new Queue<byte[]>();
    private bool canSend = false;
    public void Connect(string ip, int port)
    {
        if (socket != null)
        {
            Release();
        }
        IPAddress ipaddress = IPAddress.Parse(ip);
        IPEndPoint ipend = new IPEndPoint(ipaddress, port);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.BeginConnect(ipend, new AsyncCallback(ConnectCallBack), this.socket);
    }

    public void DisConnect()
    {
        Release();
    }

    private void ConnectCallBack(IAsyncResult ar)
    {
        try
        {
            Socket handle = (Socket)(ar.AsyncState);
            handle.EndConnect(ar);
            IsConnected = true;
            canSend = true;
            GameEvents.TriggerCheckEvent(eGameEventTypes.DataSendConnected);
        }
        catch (SocketException se)
        {
            Debug.LogError("ConnectCallBack Error " + se.ToString());
        }
    }

    public void SendBytesToQueue(byte[] byteData)
    {
        msgQueue.Enqueue(byteData);
    }

    public void update()
    {
        if (IsConnected)
        {
            if (msgQueue.Count > 0 && canSend)
            {
                byte[] msg = msgQueue.Dequeue();
                SendBytes(msg);
            }
        }
    }

    private void SendBytes(byte[] byteData)
    {
        if (!IsConnected) return;
        try
        {
            int length = byteData.Length;
            byte[] head = BitConverter.GetBytes(length);
            byte[] data = new byte[head.Length + byteData.Length];
            Debug.LogError("SendBytes" + data.Length);
            Buffer.BlockCopy(head, 0, data, 0, head.Length);
            Buffer.BlockCopy(byteData, 0, data, head.Length, byteData.Length);
            canSend = false;
            socket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(EndSendData), socket);
        }
        catch (SocketException ex)
        {
            Debug.LogError("SendBytes Error");
            GameEvents.TriggerCheckEvent(eGameEventTypes.DataSendDisConnected);
        }
    }

    public void EndSendData(IAsyncResult ar)
    {
        try
        {
            Socket handler = (Socket)(ar.AsyncState);
            handler.EndSend(ar);
            canSend = true;
        }
        catch (SocketException ex)
        {
            Debug.LogError("EndSendData Error" + ex.ToString());
        }
    }

    private void Release()
    {
        if (socket != null && socket.Connected)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        msgQueue.Clear();
        socket = null;
        IsConnected = false;
    }
}
