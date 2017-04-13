using UnityEngine;
using UnityEngine.UI;

public class LoginPanelUI : MonoBehaviour
{
    private InputField input_username;
    private InputField input_password;
    private Text label_error;
    private Button btn_Login;

    void Awake()
    {
        Init();
    }


    void Init()
    {
        Transform root = transform;
        input_username = root.Find(LoginSceneSetting.usernamePath).GetComponent<InputField>();
        input_password = root.Find(LoginSceneSetting.passwordPath).GetComponent<InputField>();
        label_error = root.Find(LoginSceneSetting.label_errorPath).GetComponent<Text>();
        btn_Login = root.Find(LoginSceneSetting.btn_loginPath).GetComponent<Button>();
        btn_Login.onClick.AddListener(OnLoginClick);



    }


    void OnEnable()
    {
        input_username.text = "";
        input_password.text = "";
        label_error.text = "";
        OpenLoginBtn();
    }

    /// <summary>
    /// 登录
    /// </summary>
    void OnLoginClick()
    {

        if (string.IsNullOrEmpty(input_username.text))
        {
            label_error.text = LoginSceneSetting.text_accountIsNull;
            return;
        }
        else if (string.IsNullOrEmpty(input_password.text))
        {
            label_error.text = LoginSceneSetting.text_passwordIsNull;
            return;
        }
        LoginFunction.Login(input_username.text, input_password.text);
        btn_Login.interactable = false;
    }

    public void OpenLoginBtn()
    {
        btn_Login.interactable = true;
    }

    //注册成功返回登录界面
    public void RegisterCallBack()
    {
        label_error.text = "注册成功";
        LoginSceneUI.Instance.ShowHideRegLogPanel();
    }

    //登录注册返回
    public void LoginCallBack(int value)
    {
        OpenLoginBtn();
        switch (value)
        {
            case 0:
                {
                    Debug.Log("登录成功");
                    GameSceneManager.LoadScene(GameSceneManager.mainSceneName);
                    break;
                }
            case -1:
                {
                    label_error.text = "帐号不存在";
                    break;
                }
            case -2:
                {
                    label_error.text = "帐号已经登录";
                    break;
                }
            case -3:
                {
                    label_error.text = "帐号密码错误";
                    break;
                }
            case -4:
                {
                    label_error.text = "帐号输入不合法";
                    break;
                }
        }
    }
}
