using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol;

public class SelectTalkUI : MonoBehaviour
{
    private static SelectTalkUI instance;

    private Text talkContent;
    private InputField talkInput;
    private Button talkSnedButton;
    private Scrollbar talkScrollBar;
    private int haveNewMsgTimer;//延迟计时器

    public static SelectTalkUI Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        Transform root = transform;
        talkContent = root.Find(SelectHeroScneSetting.talkContentPath).GetComponent<Text>();
        talkInput = root.Find(SelectHeroScneSetting.talkInputPath).GetComponent<InputField>();
        talkSnedButton = root.Find(SelectHeroScneSetting.talkSendButtonPath).GetComponent<Button>();
        talkScrollBar = root.Find(SelectHeroScneSetting.talkScrollBarPath).GetComponent<Scrollbar>();
        talkSnedButton.onClick.AddListener(SendMessage);

        talkScrollBar.onValueChanged.AddListener(WantToZero);
    }

    void Update()
    {
        if(haveNewMsgTimer>=0)
        {
            haveNewMsgTimer--;
        }
    }



    void WantToZero(float now)
    {
        if (haveNewMsgTimer >= 0 && now!=0)
        {
            talkScrollBar.value = 0;
        }
    }

    public void SendMessage()
    {
        if (!string.IsNullOrEmpty(talkInput.text))
        {
            this.WriteMessage(Protocol.TYPE_SELECT, SelectProtocol.AREA_SELECT, SelectProtocol.TALK_CREQ
                , talkInput.text);
            talkInput.text = string.Empty;
        }
    }

    public void ReceiveMessage(string value)
    {
        if (string.IsNullOrEmpty(talkContent.text))
        {
            talkContent.text += value;
        }
        else
        {
            talkContent.text += "\n" + value;
        }
        talkScrollBar.value = 0;
        haveNewMsgTimer = 2;
    }
}
