using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol.dto;

public class HeroHpBar : NormalHPBar
{

    private Slider hpSlider;
    private Slider mpSlider;
    private Text levelText;
    private Text nameText;

    void Awake()
    {
        hpSlider = transform.Find(FightSceneSetting.hpBar_hpPath).GetComponent<Slider>();
        mpSlider = transform.Find(FightSceneSetting.hpBar_mpPath).GetComponent<Slider>();
        levelText = transform.Find(FightSceneSetting.hpBar_LevelPath).GetComponent<Text>();
        nameText = transform.Find(FightSceneSetting.hpBar_namePath).GetComponent<Text>();
    }

    public virtual void InitBar(DTOFightPlayerModel model,Transform _unit)
    {
        unit = _unit;
        nameText.text = model.userID;
        Refresh(model);
    }

    public virtual void Refresh(DTOFightPlayerModel model)
    {
    
        if (unit != null)
        {
            transform.position = Camera.main.ScreenToWorldPoint(unit.position);
            hpSlider.value = model.maxHp == 0 ? 0 : model.hp / model.maxHp;
            mpSlider.value = model.mp == 0 ? 0 : model.mp / model.maxMp;
            levelText.text = model.level.ToString();
        }

    }
}