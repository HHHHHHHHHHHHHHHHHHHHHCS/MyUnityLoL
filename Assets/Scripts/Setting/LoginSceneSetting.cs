using UnityEngine;
using System.Collections;

public class LoginSceneSetting
{
    public const string topBarPath = "Bg/TopBarBg";
    public const string contentBgPath =  "Bg/ContentBg";

    public const string loginBgPath = contentBgPath + "/LoginPanelBg";
    public const string usernamePath = "UsernameLabel/UsernameInput";
    public const string passwordPath = "PasswordLabel/PasswordInput";
    public const string label_errorPath = "ErrorText";
    public const string btn_loginPath = "StartEnterGameButton";

    public const string registerBgPath = contentBgPath + "/RegisterPanelBg";
    public const string regitserUsernamePath = "UsernameLabel/UsernameInput";
    public const string regitserPasswordPath = "PasswordLabel/PasswordInput";
    public const string regitserRePasswordPath = "RePasswordLabel/RePasswordInput";
    public const string btn_registerPath = "RegisterButton";
    public const string label_registerErrorPath = "ErrorText";
    public const string btn_registerCloseBg = "CloseBgButton";

    public const string btn_topRegisterPath = topBarPath + "/RegisterButton";



    public const string text_accountIsNull = "帐号不能为空！";
    public const string text_passwordIsNull = "密码不能为空！";
    public const string text_twoPasswordNoSame = "两次密码不同！";
}
