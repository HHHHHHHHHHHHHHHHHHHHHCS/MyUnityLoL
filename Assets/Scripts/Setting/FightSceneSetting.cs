using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameProtocol.constans.Hero;
using GameProtocol.constans.Build;

public class FightSceneSetting
{
    public const string aLI_idle_Path = "Prefabs/HeroPrefab/ALi_idle";
    public const string aMuMu_idle_Path = "Prefabs/HeroPrefab/AMuMu_idle";

    public const string teamOne_1_Path = "Prefabs/BuildPrefab/TeamOne_1";
    public const string teamOne_2_Path = "Prefabs/BuildPrefab/TeamOne_2";
    public const string teamOne_3_Path = "Prefabs/BuildPrefab/TeamOne_3";

    public const string teamTwo_1_Path = "Prefabs/BuildPrefab/TeamTwo_1";
    public const string teamTwo_2_Path = "Prefabs/BuildPrefab/TeamTwo_2";
    public const string teamTwo_3_Path = "Prefabs/BuildPrefab/TeamTwo_3";


    public const string buildPos_Path = "SceneRoot/004Buildings";

    public const string teamOneStart_Path = buildPos_Path + "/TeamOneStartPos";
    public const string teamTwoStart_Path = buildPos_Path + "/TeamTwoStartPos";

    public const string normalHPBarPath = "UI/FightUI/NormalHPBar";
    public const string heroHPBarPath = "UI/FightUI/HeroHPBar";

    public const string hpBar_hpPath = "HPBar";
    public const string hpBar_namePath = "NameText";
    public const string hpBar_LevelPath = "LevelBg/LevelText";
    public const string hpBar_mpPath = "MPBar";

}

public class ObjectMapped
{
    protected Dictionary<int, string> mapDic = new Dictionary<int, string>();

    public string Find(int code)
    {
        if (mapDic.ContainsKey(code))
        {
            return mapDic[code];
        }
        return null;
    }

    public int Length()
    {
        return mapDic.Values.Count;
    }

    protected void Add(int code, string path)
    {
        mapDic.Add(code, path);
    }

}


public class HeroMapped : ObjectMapped
{
    private static HeroMapped instance;

    public static HeroMapped Instance
    {
        get
        {
            return instance;
        }
    }

    static HeroMapped()
    {
        instance = new HeroMapped();
        instance.Add(new ALi().code, FightSceneSetting.aLI_idle_Path);
        instance.Add(new AMuMu().code, FightSceneSetting.aMuMu_idle_Path);
    }


}


public class BuildMapped : ObjectMapped
{
    private static BuildMapped instance;

    public static BuildMapped Instance
    {
        get
        {
            return instance;
        }
    }

    static BuildMapped()
    {
        instance = new BuildMapped();
        instance.Add(new ZhuJiDi().code, FightSceneSetting.teamOne_1_Path);
        instance.Add(new GaoJiJianTa().code, FightSceneSetting.teamTwo_2_Path);
        instance.Add(new ZhongJiJianTa().code, FightSceneSetting.teamTwo_3_Path);

        instance.Add(-new ZhuJiDi().code, FightSceneSetting.teamTwo_1_Path);
        instance.Add(-new GaoJiJianTa().code, FightSceneSetting.teamTwo_2_Path);
        instance.Add(-new ZhongJiJianTa().code, FightSceneSetting.teamTwo_3_Path);
    }
}