using UnityEngine;
using System.Collections;
using NetFrame.auto;
using System;
using GameProtocol;

public class LoginHandler : MonoBehaviour, IHandler
{
    public void MessageReceive(SocketModel model)
    {
        switch (model.command)
        {
            case LoginProtocol.LOGIN_SRES:
                {
                    LoginHandle(model.GetMesssage<int>());
                    break;
                }
            case LoginProtocol.REG_SRES:
                {
                    RegisterHandle(model.GetMesssage<int>());
                    break;
                }
        }
    }

    void LoginHandle(int result)
    {
        LoginSceneUI.Instance.LoginPanelScript.LoginCallBack(result);
    }


    void RegisterHandle(int result)
    {
        LoginSceneUI.Instance.RegisterPanelScript.RegisterCallBack(result);
    }
}
