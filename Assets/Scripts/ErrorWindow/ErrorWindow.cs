using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ErrorWindow
{
    private static ErrorWindow _instance;

    public static ErrorWindow Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ErrorWindow();
                _instance.Init();
            }
            return _instance;
        }

    }

    private GameObject errorWindowPrefab;

    void Init()
    {
        errorWindowPrefab = Resources.Load<GameObject>(ErrorSetting.go_errorWindowPath);
    }

    public ErrorWindowInfo CreateErrorWindow(Transform parent, string errorText
        ,float destoryTime = -1, Vector2? pos = null, UnityAction errorButtonEvent = null)
    {
        if (pos == null)
        {
            pos = Vector2.zero;
        }

        GameObject newGo = (GameObject)Object.Instantiate(errorWindowPrefab,Vector3.zero,Quaternion.identity,parent);

        RectTransform rt= newGo.transform as RectTransform;
        rt.anchoredPosition = (Vector2)pos;
        ErrorWindowInfo errorWindowInfo = newGo.GetComponent<ErrorWindowInfo>();
        errorWindowInfo.Init(errorText, errorButtonEvent, destoryTime);
        return errorWindowInfo;
    }

}
