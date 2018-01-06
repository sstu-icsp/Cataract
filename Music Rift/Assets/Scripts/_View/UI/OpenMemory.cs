using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMemory : Element {

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
        app.model.player.ifOpenList = false;
    }

    public void ToggleModeSelectionPanel()
    {
        app.model.player.collectingList = true;
        app.model.player.ifOpenList = true;
        gameObject.SetActive(true);
        app.controller.game.TogglePause();
        return;
    }
    //public byte id;
    //public GameObject interfaceList;
    //private TextInfo textInfo;

    //public void ToggleModeSelectionPanel()
    //{
    //    app.model.player.collectingList = true;
    //    textInfo = interfaceList.GetComponent<TextInfo>();
    //    textInfo.gameObject.SetActive(true);
    //    textInfo.getId(id);
    //    app.controller.game.TogglePause();
    //    return;
    //}
}
