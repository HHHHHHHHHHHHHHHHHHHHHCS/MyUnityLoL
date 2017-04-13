using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ErrorWindowInfo : MonoBehaviour
{
    private Text text;
    private Button button;
    private bool hadClick;

    void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
    }

    public void Init(string errorText, UnityAction errorButtonEvent, float destoryTime)
    {
        ChangeText(errorText);
        AddEvent(errorButtonEvent);
        AddDefaultEvent();
        if (destoryTime > 0)
        {
            Destroy(gameObject, destoryTime);
        }

    }

    public void ChangeText(string errorText)
    {
        text.text = errorText;
    }

    public void AddEvent(UnityAction errorButtonEvent)
    {
        if (errorButtonEvent != null)
        {
            button.onClick.AddListener(errorButtonEvent);
        }
    }

    public void ClearEvent()
    {
        button.onClick.RemoveAllListeners();
    }

    public void AddDefaultEvent()
    {
        button.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
        hadClick = true;
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if(!hadClick)
        {
            button.onClick.RemoveListener(CloseWindow);
            button.onClick.Invoke();
        }
    }


}
