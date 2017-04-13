using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol;

public class CreatePanelUI : MonoBehaviour
{
    private InputField nameInput;
    private Text errorText;
    private Button createButton;


    void Awake()
    {
        Transform root = transform;
        nameInput = root.Find(MainSceneSetting.create_nameInputPath).GetComponent<InputField>();
        errorText = root.Find(MainSceneSetting.create_errorTextPath).GetComponent<Text>();
        createButton = root.Find(MainSceneSetting.create_createButtonPath).GetComponent<Button>();

        nameInput.onValueChanged.AddListener(CheckNameFormat);
        createButton.onClick.AddListener(Create);

        createButton.interactable = false;
    }

    public void OpenPanel()
    {

        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void EnableButton()
    {
        createButton.interactable = true;
    }

    public void Create()
    {
        CheckNameFormat(nameInput.text);

        createButton.interactable = false;
        this.WriteMessage(Protocol.TYPE_USER, UserProtocol.AREA_USER, UserProtocol.CREATE_CREQ, nameInput.text);
    }

    public void CheckNameFormat(string newName)
    {
        if (string.IsNullOrEmpty(newName) || newName.Length > 6)
        {
            errorText.text = "×格式错误";
            createButton.interactable = false;
            return;
        }
        errorText.text = "√名字合法";
        createButton.interactable = true;
    }
}
