using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using NetFrame.auto;
using GameProtocol;

public class NetIO
{

    private string ip = "127.0.0.1";

    private int port = 6650;

    private static NetIO instance;

    private Socket socket;

    private bool isConnect = false;
    private readonly static byte[] Heartbeat = { Protocol.TYPE_HEARTBEAT };

    private byte[] readBuff = new byte[1024];
    private List<byte> cache = new List<byte>();

    private bool isReading = false;

    private List<SocketModel> messages = new List<SocketModel>();

    public static NetIO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetIO();
            }
            return instance;
        }
    }

    public List<SocketModel> Messages
    {
        get
        {
            return messages;
        }

        set
        {
            messages = value;
        }
    }

    public bool IsConnect
    {
        get
        {
            return isConnect;
        }
    }

    private NetIO()
    {
        TryConnect();
    }

    public void TryConnect()
    {
        if (!IsConnect)
        {
            try
            {
                //创建客户端连接对象
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //连接到服务器
                socket.Connect(ip, port);
                //开启异步消息接受 消息到达后会直接写入 缓冲区 readBuff
                socket.BeginReceive(readBuff, 0, 1024, SocketFlags.None, ReceiveCallBack, readBuff);
                isConnect = true;
            }
            catch (Exception e)
            {
                Debug.Log("连接失败，错误原因如下");
                Debug.Log(e.Message);
            }
        }
    }

    public bool SendHeartbeat()
    {//发送心跳包
        try
        {
            socket.Send(Heartbeat); // 发送心跳包
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return false;
    }

    public void Init()
    {

    }

    public void Write(byte type, int area, int command, object message)
    {
        ByteArray ba = new ByteArray();
        ba.Write(type);
        ba.Write(area);
        ba.Write(command);
        //判断消息体是否为空 不为空则序列化后写入
        if (message != null)
        {
            ba.Write(SerializeUtil.Encode(message));
        }

        ByteArray arr1 = new ByteArray();
        arr1.Write(ba.Length);
        arr1.Write(ba.GetBuff());
        try
        {
            socket.Send(arr1.GetBuff());
        }
        catch (Exception e)
        {
            Debug.Log("网络错误！" + e.Message);
        }
    }

    //收到消息回调
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            //获取当前收到的消息长度
            int length = socket.EndReceive(ar);
            byte[] message = new byte[length];
            Buffer.BlockCopy(readBuff, 0, message, 0, length);
            cache.AddRange(message);
            if (!isReading)
            {
                isReading = true;
                OnData();
            }
            //尾递归 再次开启异步消息接受 消息到达后会直接写入缓冲区 readbuff
            socket.BeginReceive(readBuff, 0, 1024, SocketFlags.None, ReceiveCallBack, readBuff);
        }
        catch (Exception e)
        {
            Debug.Log("远程服务器主动断开连接！" + e.Message);
            CloseSocket();
        }



    }

    /// <summary>
    /// 缓存中有数据处理
    /// </summary>
    private void OnData()
    {
        //消息长度不够无法识别数据
        if (cache.Count < 4)
        {
            isReading = false;
            return;
        }

        //长度解码
        byte[] result = LengthEncoding.Decode(ref cache);


        //长度解码返回空 说明消息体不全 等待下调消息过来补全
        if (result == null)
        {
            isReading = false;
            return;
        }

        SocketModel message = MessageEncoding.Decode(result);

        if (message == null)
        {
            isReading = false;
            return;
        }

        //进行消息的处理
        Messages.Add(message);
        //尾递归 防止在消息处理过程中有其他消息到了 而没有经过处理
        OnData();
    }

    public void CloseSocket()
    {
        socket.Close();
    }
}
