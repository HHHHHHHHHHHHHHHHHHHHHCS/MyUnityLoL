using UnityEngine;
using System.Collections;
using NetFrame.auto;
using GameProtocol;

public class NetMessageUtil : MonoBehaviour
{
    private bool isConnect;

    IHandler login;
    IHandler user;
    IHandler match;
    IHandler select;
    IHandler fight;


    void Awake()
    {
        Transform root = transform;
        login = root.GetComponent<LoginHandler>();
        user = root.GetComponent<UserHandler>();
        match = root.GetComponent<MatchHandler>();
        select = root.GetComponent<SelectHandler>();
        fight = root.GetComponent<FightHandler>();
        InvokeRepeating("CheckMessage", 0, 0.001f);
    }


    void CheckMessage()
    {
        if(isConnect||NetIO.Instance.IsConnect)
        {
            isConnect = true;
        }
        else
        {
            //NetIO.Instance.TryConnect();
            return;
        }
        while (NetIO.Instance.Messages.Count > 0)
        {
            SocketModel model = NetIO.Instance.Messages[0];
            StartCoroutine("MessageReceive", model);
            NetIO.Instance.Messages.RemoveAt(0);
        }
    }

    void MessageReceive(SocketModel model)
    {
        switch (model.type)
        {
            case Protocol.TYPE_LOGIN:
                {
                    if (login != null)
                    {
                        login.MessageReceive(model);
                    }
                    break;
                }
            case Protocol.TYPE_USER:
                {
                    if (user != null)
                    {
                        user.MessageReceive(model);
                    }
                    break;
                }
            case Protocol.TYPE_MATCH:
                {
                    if (match != null)
                    {
                        match.MessageReceive(model);
                    }
                    break;
                }
            case Protocol.TYPE_SELECT:
                {
                    if(select!=null)
                    {
                        select.MessageReceive(model);
                    }
                    break;
                }
            case Protocol.TYPE_FIGHT:
                {
                    if (fight!=null)
                    {
                        fight.MessageReceive(model);
                    }
                    break;
                }
        }
    }
}
