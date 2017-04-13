using UnityEngine;
using System.Collections;
using NetFrame.auto;
using System;
using GameProtocol;
using GameProtocol.dto;

public class UserHandler : MonoBehaviour, IHandler
{
    public void MessageReceive(SocketModel model)
    {
        switch (model.command)
        {
            case UserProtocol.INFO_SRES:
                Info(model.GetMesssage<DTOUser>());
                break;
            case UserProtocol.CREATE_SRES:
                Create(model.GetMesssage<bool>());
                break;
            case UserProtocol.ONLINE_SRES:
                Online(model.GetMesssage<DTOUser>());
                break;
        }
    }

    private void Info(DTOUser user)
    {
        if (user == null)
        {
            //移除遮罩
            MainSceneUI.Instance.RemoveMask();
            //显示创建面板
            MainSceneUI.Instance.OpenCreatePanel();
        }
        else
        {
            this.WriteMessage(Protocol.TYPE_USER, UserProtocol.AREA_USER, UserProtocol.ONLINE_CREQ, null);
        }
    }

    private void Online(DTOUser user)
    {
        GameData.User = user;
        //移除遮罩
        MainSceneUI.Instance.RemoveMask();
        //刷新UI数据
        MainSceneUI.Instance.RefreshUserInfo(GameData.User);
        MainSceneUI.Instance.OpenUserInfoPanel();
        MainSceneUI.Instance.ShowMatchButton();
    }

    private void Create(bool value)
    {
        if (value)
        {

            //关闭创建面板
            MainSceneUI.Instance.CloseCreatePanel();
            MainSceneUI.Instance.CreateResultShow(MainSceneSetting.text_createSuccess
                , delegate ()
                {
                    //直接申请登录
                    this.WriteMessage(Protocol.TYPE_USER, UserProtocol.AREA_USER, UserProtocol.ONLINE_CREQ, null);
                });

        }
        else
        {
            MainSceneUI.Instance.CreateResultShow(MainSceneSetting.text_createFail);
            //开启创建面板
            MainSceneUI.Instance.EnableCreateButton();
        }
    }
}
