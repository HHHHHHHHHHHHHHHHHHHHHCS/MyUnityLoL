using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol.dto;

public class SummonSelectGrid : MonoBehaviour
{
    private Image heroImage;
    private Text summonName;
    private Image skillOneImage;
    private Image skillTwoImage;
    private Image selfImage;


    void Awake()
    {
        Transform root = transform;
        selfImage = root.GetComponent<Image>();
        heroImage = root.Find(SelectHeroScneSetting.summonSelect_heroImagePath).GetComponent<Image>();
        summonName = root.Find(SelectHeroScneSetting.summonSelect_namePath).GetComponent<Text>();
        skillOneImage=root.Find(SelectHeroScneSetting.summonSelect_skillOnePath).GetComponent<Image>();
        skillTwoImage= root.Find(SelectHeroScneSetting.summonSelect_skillTwoPath).GetComponent<Image>();
    }


    public void Refresh(DTOSelectModel model)
    {
        summonName.text = model.name;
        //是否进入
        if(model.isEnter)
        {
            //是否选择英雄
            if(model.hero==-1)
            {
                //heroImage.sprite = null;
                heroImage.gameObject.SetActive(false);
            }
            else
            {
                heroImage.gameObject.SetActive(true);
                heroImage.sprite = Resources.Load<Sprite>(SelectHeroScneSetting.heroIconsPath + model.hero);
            }
        }
        else
        {
            heroImage.sprite = Resources.Load<Sprite>(SelectHeroScneSetting.heroIcons_noEnterPath);
        }

        Selectd(model.isReady);
    }

    private void Selectd(bool  isReady)
    {//是否已经准备
        if (isReady)
        {
            selfImage.color = Color.white;
        }
        else
        {
            selfImage.color = Color.yellow;
        }
    }
}
