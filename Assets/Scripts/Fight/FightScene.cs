using UnityEngine;
using System.Collections;
using GameProtocol;


public class FightScene : MonoBehaviour
{
    private static FightScene instance;

    private Transform[] buildPosArray;//初始建筑位置表

    private GameObject[] teamOneBuild;
    private GameObject[] teamTwoBuild;

    private Transform[] teamStartPos;

    public static FightScene Instance
    {
        get
        {
            return instance;
        }
    }

    public Transform[] BuildPosArray
    {
        get
        {
            return buildPosArray;
        }
    }

    public Transform[] TeamStartPos
    {
        get
        {
            return teamStartPos;
        }
    }

    void Awake()
    {
        instance = this;

        Transform startPos_1 = GameObject.Find(FightSceneSetting.teamOneStart_Path).transform;
        Transform startPos_2 = GameObject.Find(FightSceneSetting.teamTwoStart_Path).transform;
        teamStartPos = new Transform[] { startPos_1, startPos_2 };


        buildPosArray = new Transform[6];
        for (int i = 0; i < 3; i++)
        {
            BuildPosArray[i] = GameObject.Find(string.Format("{0}/TeamOne_{1}_Pos", FightSceneSetting.buildPos_Path, i + 1)).transform;
            BuildPosArray[3 + i] = GameObject.Find(string.Format("{0}/TeamTwo_{1}_Pos", FightSceneSetting.buildPos_Path, i + 1)).transform;
        }


    }

    void Start()
    {
        this.WriteMessage(Protocol.TYPE_FIGHT, FightProtocol.AREA_FIGHT, FightProtocol.ENTER_CREQ, null);
    }


    private void BuildAddScript()
    {

    }

}
