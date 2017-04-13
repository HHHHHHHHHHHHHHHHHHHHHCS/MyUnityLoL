using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol.dto;

public class NormalHPBar : MonoBehaviour
{
    public Transform unit;

    private Slider hpSlider;

    void Awake()
    {
        hpSlider = transform.Find( FightSceneSetting.hpBar_hpPath).GetComponent<Slider>();
    }

    public virtual void InitBar(AbsFightModel model,Transform _unit)
    {
        unit = _unit;
        Refresh(model);
    }

    public virtual void Refresh(AbsFightModel model)
    {
        if (unit != null)
        {
            transform.position = Camera.main.ScreenToWorldPoint(unit.position);
            hpSlider.value = model.maxHp==0?0: model.hp / model.maxHp;
        }
    }

    public virtual void Destory()
    {

    }

}
