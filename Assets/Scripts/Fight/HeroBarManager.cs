using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameProtocol.dto;

public class HpBarManager : MonoBehaviour
{
    private static HpBarManager instance;

    public static HpBarManager Instance
    {
        get
        {
            return instance;
        }
    }

    private Dictionary<AbsFightModel, NormalHPBar> normalDic = new Dictionary<AbsFightModel, NormalHPBar>();
    private Dictionary<DTOFightPlayerModel, HeroHpBar> heroDic = new Dictionary<DTOFightPlayerModel, HeroHpBar>();

    public void Awake()
    {
        instance = this;

    }

    void Update()
    {
        foreach(HeroHpBar item in FightGameManager.Instance.HeroHPBarList)
        {
           if(item!=null)
            {
                //TODO:
            }
        }

        foreach (NormalHPBar item in FightGameManager.Instance.NormalHPBarList)
        {

        }
    }
}
