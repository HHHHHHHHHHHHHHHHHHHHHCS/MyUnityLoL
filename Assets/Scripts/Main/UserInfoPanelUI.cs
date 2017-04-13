using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol.dto;
using GameProtocol;

/// <summary>
/// 角色信息面板UI
/// </summary>
public class UserInfoPanelUI : MonoBehaviour
{
    private Text nameText;
    private Text levelText;
    private Slider expSlider;
    private Text expText;


    public void Awake()
    {
        Transform root = transform;
        nameText = root.Find(MainSceneSetting.info_nameTextPath).GetComponent<Text>();
        levelText = root.Find(MainSceneSetting.info_levelTextPath).GetComponent<Text>();
        expSlider = root.Find(MainSceneSetting.info_expSliderPath).GetComponent<Slider>();
        expText = root.Find(MainSceneSetting.info_expTextPath).GetComponent<Text>();

    }


    public void RefreshUserInfo(DTOUser user)
    {
        nameText.text = user.name;
        levelText.text = string.Format("LV.{0}", user.level); ;
        expSlider.value = (float)user.nowExp / (user.level * 100);
        expText.text = string.Format("{0}/{1}", user.nowExp, user.level * 100);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }


}
