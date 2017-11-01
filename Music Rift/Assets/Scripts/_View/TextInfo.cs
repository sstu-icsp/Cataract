using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInfo : MonoBehaviour {

    private string titleList = "блаблабла";
    private string textList = "туруру";

    public Button closeButton;

    void Awake()
    {
        closeButton.onClick.AddListener(Close);
    }

    void Close()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    public void getText()
    {
        gameObject.GetComponentsInChildren<Text>()[0].text = titleList;
        gameObject.GetComponentsInChildren<Text>()[1].text = textList;
    }

    public void getId(byte id)
    {
        switch (id)
        {
            case 1:
                {
                    titleList = "Начало";
                    textList = "\tЧто за странные звуки? Похоже на мелодию, но я её не слышал прежде. " +
                        "Похоже эксперимент провалился. Но что-то произошло и нужно с этим разобраться...";
                } break;
            case 2:
                {
                    titleList = "Галлюцинации";
                    textList = "\tЧто это только что было?! Что это за странная галлюцинация??? " + 
                        "Я увидел мелодию? Похоже пора хорошенько проспаться.";
                } break;
        }
        getText();
    }
}
