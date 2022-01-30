using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;



public class Connect_Client : MonoBehaviour
{
    Messages mess = new Messages();
    Socket client;
    public string data;
    public static Connect_Client _instance;

    private void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ConnectAsync();
            Debug.Log("connect");
        }
        if (mess.GetData.Length != 0)
        {
            data = mess.GetData;
        }

    }

    private void ConnectAsync()
    {
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11111);
        client.Connect(ipEndPoint);
        if (client.Connected) Debug.Log("connected success !");
        client.Send(Encoding.UTF8.GetBytes("hello server"));
        client.BeginReceive(mess.Data, mess.StartIndex, mess.RemainSize, SocketFlags.None, ReceviceCallBack, client);
    }
    private void ReceviceCallBack(IAsyncResult ar)
    {
        Socket client = null;
        try
        {
            client = ar.AsyncState as Socket;
            int count = client.EndReceive(ar);  //读取到的数据的长度
            if (count == 0)
            {
                client.Close();
                return;
            }
            mess.AddCound(count);
            mess.ReadMessage();
            client.BeginReceive(mess.Data, mess.StartIndex, mess.RemainSize, SocketFlags.None, ReceviceCallBack, client);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void OnApplicationQuit()
    {
        client.Close();
        Debug.Log("quit");
    }
}
