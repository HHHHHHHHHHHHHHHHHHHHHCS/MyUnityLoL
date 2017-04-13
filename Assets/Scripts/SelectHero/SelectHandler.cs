using NetFrame.auto;
using GameProtocol;
using GameProtocol.dto;
using UnityEngine;

public class SelectHandler : MonoBehaviour, IHandler
{
    private DTOSelectRoom room;


    public void MessageReceive(SocketModel model)
    {
        switch (model.command)
        {
            case SelectProtocol.DESTROY_BRO:
                {
                    GameSceneManager.LoadScene(GameSceneManager.mainSceneName);
                    break;
                }
            case SelectProtocol.ENTER_SRES:
                {
                    Enter(model.GetMesssage<DTOSelectRoom>());
                    break;
                }

            case SelectProtocol.ENTER_EXBRO:
                {
                    OtherEnter(model.GetMesssage<string>());
                    break;
                }
            case SelectProtocol.FIGHT_BRO:
                {
                    Fight();
                    break;
                }
            case SelectProtocol.READY_BRO:
                {
                    Ready(model.GetMesssage<DTOSelectModel>());
                    break;
                }
            case SelectProtocol.SELECT_BRO:
                {
                    Select(model.GetMesssage<DTOSelectModel>());
                    break;
                }
            case SelectProtocol.SELECT_SRES:
                {
                    SelectError();
                    break;
                }
            case SelectProtocol.TALK_BRO:
                {
                    Talk(model.GetMesssage<string>());
                    break;
                }

        }

    }


    private void Enter(DTOSelectRoom dto)
    {
        room = dto;
        //刷新界面UI
        SelectEventUtil.refreshVive(room);
        SelectHeroSceneUI.Instance.CloseMask();
    }

    private void OtherEnter(string userID)
    {
        if (room == null)
        {
            return;
        }
        foreach (DTOSelectModel item in room.teamOne)
        {
            if (item.userID == userID)
            {
                item.isEnter = true;
                //刷新UI界面
                SelectEventUtil.refreshVive(room);
            }
        }
        foreach (DTOSelectModel item in room.teamTwo)
        {
            if (item.userID == userID)
            {
                item.isEnter = true;
                //刷新UI界面
                SelectEventUtil.refreshVive(room);
            }
        }
    }


    private void Talk(string value)
    {
        //向聊天面板添加信息
        SelectTalkUI.Instance.ReceiveMessage(value);
    }

    private void Fight()
    {
        GameSceneManager.LoadScene(GameSceneManager.fightSceneName);
    }

    private void SelectError()
    {
        SelectHeroSceneUI.Instance.SelectHeroError();
    }

    private void Select(DTOSelectModel model)
    {
        foreach (DTOSelectModel item in room.teamOne)
        {
            if (item.userID == model.userID)
            {
                item.hero = model.hero;
                //刷新UI界面
                SelectEventUtil.refreshVive(room);
                return;
            }
        }
        foreach (DTOSelectModel item in room.teamTwo)
        {
            if (item.userID == model.userID)
            {
                item.hero = model.hero;
                //刷新UI界面
                SelectEventUtil.refreshVive(room);
                return;
            }
        }
    }


    private void Ready(DTOSelectModel model)
    {
        if (model.userID == GameData.User.id)
        {
            //是自己准备了  进行状态处理  不准点击操作了
            SelectEventUtil.selected();
        }
        foreach (DTOSelectModel item in room.teamOne)
        {
            if (item.userID == model.userID)
            {
                item.hero = model.hero;
                item.isReady = true;
                //刷新UI界面
                SelectEventUtil.refreshVive(room);
                return;
            }
        }
        foreach (DTOSelectModel item in room.teamTwo)
        {
            if (item.userID == model.userID)
            {
                item.hero = model.hero;
                item.isReady = true;
                //刷新UI界面
                SelectEventUtil.refreshVive(room);
                return;
            }
        }
    }

}
