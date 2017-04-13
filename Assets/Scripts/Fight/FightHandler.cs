using UnityEngine;
using System.Collections;
using NetFrame.auto;
using System;
using GameProtocol;
using GameProtocol.dto;
using System.Collections.Generic;

public class FightHandler : MonoBehaviour, IHandler
{
    DTOFightRoomModel room;



    private Dictionary<int, GameObject> teamOne = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> teamTwo = new Dictionary<int, GameObject>();

    public void MessageReceive(SocketModel model)
    {
        switch (model.command)
        {
            case FightProtocol.START_BRO:
                {
                    StartGame(model.GetMesssage<DTOFightRoomModel>());
                    break;
                }
        }

    }


    private void StartGame(DTOFightRoomModel value)
    {
        room = value;

        foreach (AbsFightModel item in value.teameOne)
        {
            if (item.fightUnitType == FightUnitType.HERO)
            {
                GameObject hero = (GameObject)Instantiate(Resources.Load<GameObject>(HeroMapped.Instance.Find(item.code))
                    , FightScene.Instance.TeamStartPos[0].position
                        + new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5))
                    , FightScene.Instance.TeamStartPos[0].rotation);
                teamOne.Add(item.id, hero);
            }
            else if (item.fightUnitType == FightUnitType.BUILD)
            {
                GameObject build = (GameObject)Instantiate(Resources.Load<GameObject>(BuildMapped.Instance.Find(item.code))
                    , FightScene.Instance.BuildPosArray[item.code - 1].position
                    , FightScene.Instance.BuildPosArray[item.code - 1].rotation);
                teamOne.Add(item.id, build);
            }
        }

        int endIndex = FightScene.Instance.BuildPosArray.Length / 2 - 1;
        foreach (AbsFightModel item in value.teameTwo)
        {
            if (item.fightUnitType == FightUnitType.HERO)
            {
                GameObject hero = (GameObject)Instantiate(Resources.Load<GameObject>(HeroMapped.Instance.Find(item.code))
                     , FightScene.Instance.TeamStartPos[1].position
                        + new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5))
                    , FightScene.Instance.TeamStartPos[1].rotation);
                teamTwo.Add(item.id, hero);
            }
            else if (item.fightUnitType == FightUnitType.BUILD)
            {
                GameObject build = (GameObject)Instantiate(Resources.Load<GameObject>(BuildMapped.Instance.Find(-item.code))
                    , FightScene.Instance.BuildPosArray[endIndex + item.code].position
                    , FightScene.Instance.BuildPosArray[endIndex + item.code].rotation);
                teamTwo.Add(item.id, build);
            }
        }
    }
}
