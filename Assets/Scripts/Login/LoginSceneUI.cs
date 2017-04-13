using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginSceneUI : MonoBehaviour
{
    private static LoginSceneUI _instance;

    public static LoginSceneUI Instance
    {
        get
        {
            return _instance;
        }
    }

    public LoginPanelUI LoginPanelScript
    {
        get
        {
            return loginPanelScript;
        }
    }

    public RegisterPanelUI RegisterPanelScript
    {
        get
        {
            return registerPanelScript;
        }
    }

    private GameObject loginPanelBg;
    private GameObject registerPanelBg;

    private LoginPanelUI loginPanelScript;
    private RegisterPanelUI registerPanelScript;

    private Button btn_topRegister;

    void Awake()
    {
        _instance = this;
        Init();
    }

    void Init()
    {
        Transform root = transform;
        loginPanelBg = root.Find(LoginSceneSetting.loginBgPath).gameObject;
        registerPanelBg = root.Find(LoginSceneSetting.registerBgPath).gameObject;
        loginPanelScript = loginPanelBg.GetComponent<LoginPanelUI>();
        registerPanelScript = registerPanelBg   .GetComponent<RegisterPanelUI>();
        btn_topRegister = root.Find(LoginSceneSetting.btn_topRegisterPath).GetComponent<Button>();
        btn_topRegister.onClick.AddListener(ShowHideRegLogPanel);
        ShowHideRegLogPanel();
    }

    /// <summary>
    /// 显示隐藏登录和注册的界面
    /// </summary>
    public void ShowHideRegLogPanel()
    {
        if (registerPanelBg.activeSelf == true)
        {
            loginPanelBg.SetActive(true);
            registerPanelBg.SetActive(false);
        }
        else
        {
            loginPanelBg.SetActive(false);
            registerPanelBg.SetActive(true);
        }
    }
}
