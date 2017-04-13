using UnityEngine;
using NetFrame.auto;
using GameProtocol;

public class MatchHandler : MonoBehaviour, IHandler
{
    public void MessageReceive(SocketModel model)
    {
        if(model.command==MatchProtocol.ENTER_SELECT_BRO)
        {
            GameSceneManager.LoadScene(GameSceneManager.selectHeroSceneName);
        }
    }
}
