using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.IO;

public class SyncDataManager
{
    public DataSender Sender;
    public DataReceiver Receiver;

    public void Update()
    {
        if (Sender != null)
            Sender.update();
        if (Receiver != null)
            Receiver.update();
    }

    public void BeginConnect(string ip, int port)
    {
        if (Sender == null)
            Sender = new DataSender();
        Sender.Connect(ip, port);
    }

    public void StopConnect()
    {
        if (Sender == null)
            Sender = new DataSender();
        Sender.DisConnect();
    }

    List<string> alllocalFiles = new List<string>();

    public void SendLoalData()
    {
        alllocalFiles.Clear();
        string historyDataPath = Application.persistentDataPath + "/HistoryData";
        GetAllLocalFiles(historyDataPath);
        foreach (var lf in alllocalFiles)
        {
            SyncData data = new SyncData();
            data.RelativePath = lf.Replace(historyDataPath, "").Replace("\\", "/");
            data.Data = File.ReadAllBytes(lf);
            SendData(data);
        }
        SyncData enddata = new SyncData();
        enddata.RelativePath = "over";
        enddata.Data = BitConverter.GetBytes(1);
        SendData(enddata);
        GameEvents.TriggerCheckEvent(eGameEventTypes.DataSenderSyncOver);
    }

    private void GetAllLocalFiles(string path)
    {
        string[] dirs = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);
        foreach (var f in files)
        {
            alllocalFiles.Add(f);
        }
        foreach (var d in dirs)
        {
            GetAllLocalFiles(d);
        }
    }

    private void SendData(SyncData data)
    {
        byte[] pathBytes = System.Text.Encoding.Default.GetBytes(data.RelativePath);
        int pathLength = pathBytes.Length;
        byte[] pathLengthBytes = BitConverter.GetBytes(pathLength);
        int dataLenth = data.Data.Length;
        byte[] dataLengthBytes = BitConverter.GetBytes(dataLenth);
        byte[] finalData = new byte[pathLengthBytes.Length + pathLength + dataLengthBytes.Length + dataLenth];
        int destOffset = 0;
        Buffer.BlockCopy(pathLengthBytes, 0, finalData, destOffset, pathLengthBytes.Length);
        destOffset += pathLengthBytes.Length;
        Buffer.BlockCopy(pathBytes, 0, finalData, destOffset, pathBytes.Length);
        destOffset += pathBytes.Length;
        Buffer.BlockCopy(dataLengthBytes, 0, finalData, destOffset, dataLengthBytes.Length);
        destOffset += dataLengthBytes.Length;
        Buffer.BlockCopy(data.Data, 0, finalData, destOffset, data.Data.Length);
        Sender.SendBytesToQueue(finalData);
    }

    public void StartListen(int port)
    {
        if (Receiver == null)
            Receiver = new DataReceiver(ProcessReceivedData);
        Receiver.StartListen(port);
    }

    public void StopListen()
    {
        if (Receiver == null)
            Receiver = new DataReceiver(ProcessReceivedData);
        Receiver.ClosedListen();
    }

    public void ProcessReceivedData(byte[] formatDataBytes)
    {
        SyncData data = new SyncData();
        int srcOffset = 0;
        int pathByteLength = BitConverter.ToInt32(formatDataBytes, 0);
        srcOffset += 4;
        byte[] relatePathBytes = new byte[pathByteLength];
        Buffer.BlockCopy(formatDataBytes, srcOffset, relatePathBytes, 0,pathByteLength);
        string relatePath = System.Text.Encoding.Default.GetString(relatePathBytes);
        srcOffset += pathByteLength;
        int dataBytesLength = BitConverter.ToInt32(formatDataBytes, srcOffset);
        srcOffset += 4;
        byte[] dataBytes = new byte[dataBytesLength];
        Buffer.BlockCopy(formatDataBytes, srcOffset, dataBytes, 0, dataBytesLength);
        data.RelativePath = relatePath;
        data.Data = dataBytes;
        if (data.RelativePath == "over")
        {
            GameEvents.TriggerCheckEvent(eGameEventTypes.DataReceiverSyncOver);
        }
        else
        {
            SaveData(data);

        }
    }

    private void SaveData(SyncData data)
    {
        string historyDataPath = Application.persistentDataPath + "/HistoryData";
        string filename = historyDataPath + data.RelativePath;
        Utility.CreateDir(filename);
        File.WriteAllBytes(filename, data.Data);
    }
}
