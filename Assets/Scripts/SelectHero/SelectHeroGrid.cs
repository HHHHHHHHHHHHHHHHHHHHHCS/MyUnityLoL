using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol;

public class SelectHeroGrid : MonoBehaviour
{
    private Button btn;
    private Image img;
    private int id = -1;

    void Awake()
    {
        img = transform.GetChild(0).GetComponent<Image>();
        btn = transform.GetChild(0).GetComponent<Button>();
    }

    public void Init(int _id)
    {
        id = _id;
        img.sprite = Resources.Load<Sprite>(SelectHeroScneSetting.heroIconsPath+id);
        btn.onClick.AddListener(Click);
    }

    public void Active()
    {
        btn.interactable = true;
    }

    public void Deactive()
    {
        btn.interactable = false;
    }

    public void Click()
    {
        if (id != -1)
        {//处理角色选择事件
            this.WriteMessage(Protocol.TYPE_SELECT, SelectProtocol.AREA_SELECT, SelectProtocol.SELECT_CREQ, id);
        }
    }
}
