using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInfo : Element {

    private string titleList = "Здесь ничего нет";
    private string textList = "";

    public Button closeButton;

    void Awake()
    {
        closeButton.onClick.AddListener(Close);
    }

    void Close()
    {
        app.controller.game.TogglePause();
        gameObject.SetActive(false);
        app.model.player.collectingList = false;
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
                    titleList = "1 Начало";
                    textList = "\tЧто за странные звуки? Похоже на мелодию, но я её не слышал прежде. " +
                        "Похоже эксперимент провалился. Но что-то произошло и нужно с этим разобраться...";
                } break;
            case 2:
                {
                    titleList = "2 Галлюцинации";
                    textList = "\tЧто это только что было?! Что это за странная галлюцинация??? " + 
                        "Я увидел мелодию? Похоже пора хорошенько проспаться.";
                } break;
        }
        getText();
    }
}
