using UnityEngine;
using GameProtocol;
using System.Collections;
using UnityEngine.UI;
using GameProtocol.dto;
using UnityEngine.Events;

public class MainSceneUI : MonoBehaviour
{
    private static MainSceneUI instance;

    private Transform bg;//背景
    private GameObject mask;//遮罩 防止用户误操作 顶层遮罩
    private UserInfoPanelUI userInfoPanel;//帐号角色信息面板
    private CreatePanelUI createAccountPanel;//创建角色面板


    private Button matchButton;//匹配按钮
    private Text matchText;//匹配文字
    private bool isMatch;//是否在匹配

    public static MainSceneUI Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        Init();
        if (GameData.User == null)
        {
            mask.SetActive(true);
            //向服务器要数据
            this.WriteMessage(Protocol.TYPE_USER, UserProtocol.AREA_USER
                , UserProtocol.INFO_CREQ, null);
        }
    }

    void Init()
    {
        instance = this;
        Transform root = transform;

        bg = transform.Find(MainSceneSetting.bgPath);

        mask = root.Find(MainSceneSetting.maskPath).gameObject;
        mask.SetActive(false);
        userInfoPanel = root.Find(MainSceneSetting.userInfoPath).GetComponent<UserInfoPanelUI>();
        createAccountPanel = root.Find(MainSceneSetting.createAccountPath).GetComponent<CreatePanelUI>();


        matchButton = root.Find(MainSceneSetting.matchButtonPath).GetComponent<Button>();
        matchText = root.Find(MainSceneSetting.matchTextPath).GetComponent<Text>();

        matchButton.onClick.AddListener(Match);
        RefreshMatchText();
        HideMatchButton();
    }

    void Start()
    {

        CloseCreatePanel();
        CloseUserInfoPanel();
    }

    public void RemoveMask()
    {
        mask.SetActive(false);
    }

    public void ShowMatchButton()
    {
        matchButton.gameObject.SetActive(true);
    }

    public void HideMatchButton()
    {
        matchButton.gameObject.SetActive(false);
    }
    
    public void OpenCreatePanel()
    {
        createAccountPanel.OpenPanel();
    }

    public void CloseCreatePanel()
    {
        createAccountPanel.ClosePanel();
    }

    public void EnableCreateButton()
    {
        createAccountPanel.EnableButton();
    }

    public void RefreshUserInfo(DTOUser user)
    {
        userInfoPanel.RefreshUserInfo(user);
    }

    public void OpenUserInfoPanel()
    {
        userInfoPanel.OpenPanel();
    }

    public void CloseUserInfoPanel()
    {
        userInfoPanel.ClosePanel();
    }


    public void Match()
    {
        isMatch = !isMatch;
        RefreshMatchText();
        if (isMatch)
        {
            this.WriteMessage(Protocol.TYPE_MATCH, MatchProtocol.AREA_MATCH, MatchProtocol.ENTER_CREQ, null);
        }
        else
        {
            this.WriteMessage(Protocol.TYPE_MATCH, MatchProtocol.AREA_MATCH, MatchProtocol.LEAVE_CREQ, null);
        }
    }

    private void RefreshMatchText()
    {
        if (isMatch)
        {
            matchText.text = MainSceneSetting.text_levelMatch;
        }
        else
        {
            matchText.text = MainSceneSetting.text_enterMatch;
        }
    }


    public void CreateResultShow(string text,UnityAction callback=null)
    {
        ErrorWindow.Instance.CreateErrorWindow(bg, text,5,null, callback);
    }

}
