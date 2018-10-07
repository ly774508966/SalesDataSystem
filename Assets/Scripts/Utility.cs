using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Utility
{
    public static void CreateDir(string path)
    {
        string parent = Path.GetDirectoryName(path);
        if (!Directory.Exists(parent))
        {
            Directory.CreateDirectory(parent);
            CreateDir(parent);
        }
    }

    public static IPAddress GetLocalIpv4()
    {
        string hostName = Dns.GetHostName();
        IPAddress result = null;
        IPAddress[] ipaddresses = Dns.GetHostAddresses(hostName);
        foreach (var ad in ipaddresses)
        {
            if (ad.AddressFamily == AddressFamily.InterNetwork)
            {
                result = ad;
            }
        }
        return result;
    }
}
