using UnityEngine;
using System.Collections;
using GameProtocol;
using System.Collections.Generic;
using GameProtocol.dto;
using UnityEngine.UI;

public class SelectHeroSceneUI : MonoBehaviour
{
    private static SelectHeroSceneUI instance;

    public static SelectHeroSceneUI Instance
    {
        get
        {
            return instance;
        }
    }

    private Transform bg;
    private GameObject heroGridPrefab;//英雄按钮的prefab
    private Transform heroContentGrids;//初始化英雄列表
    private GameObject InitMask;//防止玩家误操作遮罩

    private SummonSelectGrid[] myTeamSummonGrids;//我的队伍列表
    private SummonSelectGrid[] enemyTeamSummonGrids;//敌人队伍列表

    private bool isSelected;
    private Button readyButton;

    //英雄ID和按钮信息的映射
    private Dictionary<int, SelectHeroGrid> gridMap = new Dictionary<int, SelectHeroGrid>();

    void Awake()
    {
        instance = this;

        SelectEventUtil.selected = Selected;
        SelectEventUtil.refreshVive = RefreshVive;

        Transform root = transform;
        bg = root.Find(SelectHeroScneSetting.bgPath);

        InitMask = root.Find(SelectHeroScneSetting.maskPath).gameObject;

        heroContentGrids = root.Find(SelectHeroScneSetting.heroContentGridsPath);

        readyButton = root.Find(SelectHeroScneSetting.readyButton).GetComponent<Button>();
        readyButton.onClick.AddListener(ReadyClick);

        heroGridPrefab = Resources.Load<GameObject>(SelectHeroScneSetting.selectHeroGridPath);

        Transform myTeamTS = root.Find(SelectHeroScneSetting.myTeamPath);
        myTeamSummonGrids = new SummonSelectGrid[myTeamTS.childCount - 1];
        for (int i = 1; i < myTeamTS.childCount; i++)
        {
            myTeamSummonGrids[i - 1] = myTeamTS.Find(SelectHeroScneSetting.myTeamSummonGridPath + i).GetComponent<SummonSelectGrid>();
            myTeamSummonGrids[i - 1].gameObject.SetActive(false);
        }
        Transform enemyTeamTS = root.Find(SelectHeroScneSetting.enemyTeamPath);
        enemyTeamSummonGrids = new SummonSelectGrid[enemyTeamTS.childCount - 1];
        for (int i = 1; i < enemyTeamTS.childCount; i++)
        {
            enemyTeamSummonGrids[i - 1] = enemyTeamTS.Find(SelectHeroScneSetting.enemyTeamSummonGridPath + i).GetComponent<SummonSelectGrid>();
            enemyTeamSummonGrids[i - 1].gameObject.SetActive(false);
        }
    }


    // Use this for initialization
    void Start()
    {
        //显示遮罩防止误操作
        InitMask.SetActive(true);
        //初始化英雄列表
        InitHeroList();
        //通知进入场景并且加载完成
        this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.ENTER_CREQ, null);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SelectHeroError()
    {
        ErrorWindow.Instance.CreateErrorWindow(bg, SelectHeroScneSetting.text_selectHeroError, 3f);
    }

    private void InitHeroList()
    {
        if (GameData.User == null)
        {//如果还没有登录则说明进行了异常操作
            return;
        }
        foreach (int item in GameData.User.heroList)
        {
            //创建英雄头像并添加进选择列表
            GameObject btn = Instantiate(heroGridPrefab);
            btn.transform.SetParent(heroContentGrids);
            btn.transform.localScale = Vector3.one;
            SelectHeroGrid grid = btn.GetComponent<SelectHeroGrid>();
            grid.Init(item);
            gridMap.Add(item, grid);
        }

    }

    public void CloseMask()
    {
        if (InitMask.activeSelf)
        {
            InitMask.SetActive(false);
        }
    }

    public void RefreshVive(DTOSelectRoom room)
    {//刷新界面 包括队伍列表和英雄选择界面
        int team = room.GetTeamByUserID(GameData.User.id);
        //如果自身在队伍一 则队伍一显示在左边  否则 队伍二显示在左边
        if (team == 1)
        {
            for (int i = 0; i < room.teamOne.Length; i++)
            {
                myTeamSummonGrids[i].Refresh(room.teamOne[i]);
                if (!myTeamSummonGrids[i].gameObject.activeSelf)
                {
                    myTeamSummonGrids[i].gameObject.SetActive(true);
                }
            }
            for (int i = 0; i < room.teamTwo.Length; i++)
            {
                enemyTeamSummonGrids[i].Refresh(room.teamTwo[i]);
                if (!enemyTeamSummonGrids[i].gameObject.activeSelf)
                {
                    enemyTeamSummonGrids[i].gameObject.SetActive(true);
                }
            }
        }
        else if (team == 2)
        {
            for (int i = 0; i < room.teamTwo.Length; i++)
            {
                myTeamSummonGrids[i].Refresh(room.teamTwo[i]);
                if (!myTeamSummonGrids[i].gameObject.activeSelf)
                {
                    myTeamSummonGrids[i].gameObject.SetActive(true);
                }
            }
            for (int i = 0; i < room.teamOne.Length; i++)
            {
                enemyTeamSummonGrids[i].Refresh(room.teamOne[i]);
                if (!enemyTeamSummonGrids[i].gameObject.activeSelf)
                {
                    enemyTeamSummonGrids[i].gameObject.SetActive(true);
                }
            }
        }
        RefreshHeroList(room);
    }

    private void RefreshHeroList(DTOSelectRoom room)
    {//刷新英雄选择界面
        int team = room.GetTeamByUserID(GameData.User.id);
        List<int> selectedList = new List<int>();
        if (team == 1)
        {
            foreach (DTOSelectModel item in room.teamOne)
            {
                if (item.hero != -1)
                {
                    selectedList.Add(item.hero);
                }
            }
        }
        else if (team == 2)
        {
            foreach (DTOSelectModel item in room.teamTwo)
            {
                if (item.hero != -1)
                {
                    selectedList.Add(item.hero);
                }
            }
        }

        //讲己选英雄从选择才对那中设置状态不可选状态
        foreach (int item in gridMap.Keys)
        {
            if (selectedList.Contains(item) || isSelected)
            {
                gridMap[item].Deactive();
            }
            else
            {
                gridMap[item].Active();
            }
        }
    }


    public void Selected()
    {
        if (!isSelected)
        {
            isSelected = true;
            readyButton.interactable = false;
        }
    }



    private void ReadyClick()
    {
        this.WriteMessage(Protocol.TYPE_SELECT, SelectProtocol.AREA_SELECT, SelectProtocol.READY_CREQ, null);
    }
}
