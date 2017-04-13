using UnityEngine;
using System.Collections;
using GameProtocol.dto;
using GameProtocol;

public class LoginFunction
{

    public static void Login(string account, string password)
    {
        DTOAccountInfo message = new DTOAccountInfo()
        {
            Account = account,
            Password = password
        };
        NetIO.Instance.Write(Protocol.TYPE_LOGIN, LoginProtocol.AREA_LOGIN
            , LoginProtocol.LOGIN_CREQ, message);
    }

    public static void Register(string account, string password)
    {
        DTOAccountInfo message = new DTOAccountInfo()
        {
            Account = account,
            Password = password
        };
        NetIO.Instance.Write(Protocol.TYPE_LOGIN, LoginProtocol.AREA_LOGIN
            , LoginProtocol.REG_CREQ, message);
    }
}
