using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPanelUI : MonoBehaviour
{
    private InputField input_username;
    private InputField input_password;
    private InputField input_rePassword;
    private Text label_error;
    private Button btn_register;
    private Button btn_closeBg;

    void Awake()
    {
        Init();
    }


    void Init()
    {
        Transform root = transform;
        input_username = root.Find(LoginSceneSetting.regitserUsernamePath).GetComponent<InputField>();
        input_password = root.Find(LoginSceneSetting.regitserPasswordPath).GetComponent<InputField>();
        input_rePassword = root.Find(LoginSceneSetting.regitserRePasswordPath).GetComponent<InputField>();
        label_error = root.Find(LoginSceneSetting.label_registerErrorPath).GetComponent<Text>();
        btn_register = root.Find(LoginSceneSetting.btn_registerPath).GetComponent<Button>();
        btn_register.onClick.AddListener(OnRegisterClick);
        btn_closeBg = root.Find(LoginSceneSetting.btn_registerCloseBg).GetComponent<Button>();
        btn_closeBg.onClick.AddListener(OnCloseClick);
    }

    void OnEnable()
    {
        input_username.text = "";
        input_password.text = "";
        input_rePassword.text = "";
        label_error.text = "";
        OpenRegisterBtn();
    }


    void OnRegisterClick()
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
        else if (input_password.text != input_rePassword.text)
        {
            label_error.text = LoginSceneSetting.text_twoPasswordNoSame;
            return;
        }
        LoginFunction.Register(input_username.text, input_password.text);
        btn_register.interactable = false;
    }



    void OnCloseClick()
    {
        LoginSceneUI.Instance.ShowHideRegLogPanel();
    }

    public void OpenRegisterBtn()
    {
        btn_register.interactable = true;
    }

    public void RegisterCallBack(int value)
    {
        OpenRegisterBtn();
        switch (value)
        {
            case 0:
                {
                    Debug.Log("注册成功");
                    LoginSceneUI.Instance.LoginPanelScript.RegisterCallBack();
                    break;
                }
            case -1:
                {
                    label_error.text = "帐号已经存在";
                    break;
                }
            case -2:
                {
                    label_error.text = "帐号不合法";
                    break;
                }
            case -3:
                {
                    label_error.text = "密码不合法";
                    break;
                }
        }
    }
}
