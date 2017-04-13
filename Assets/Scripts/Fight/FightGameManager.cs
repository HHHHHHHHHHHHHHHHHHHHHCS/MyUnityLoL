using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightGameManager : MonoBehaviour
{
    private static FightGameManager _instance;

    private List<NormalHPBar> normalHPBarList = new List<NormalHPBar>();
    private List<HeroHpBar> heroHPBarList = new List<HeroHpBar>();


    public static FightGameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<NormalHPBar> NormalHPBarList
    {
        get
        {
            return normalHPBarList;
        }
    }

    public List<HeroHpBar> HeroHPBarList
    {
        get
        {
            return heroHPBarList;
        }
    }

    void Awake()
    {
        _instance = this;
    }

}
