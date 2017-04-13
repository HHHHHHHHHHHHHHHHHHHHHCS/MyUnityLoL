using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public const string loginSceneName = "LoginScene";
    public const string mainSceneName = "MainScene";
    public const string selectHeroSceneName = "SelectHeroScene";
    public const string fightSceneName = "FightScene";

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
