using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTutorial : Element {
    public byte id;
    public GameObject interfaceList;
    private TextInfo textInfo;

    public void ToggleModeSelectionPanel()
    {
        app.model.player.collectingList = true;
        textInfo = interfaceList.GetComponent<TextInfo>();
        textInfo.gameObject.SetActive(true);
        textInfo.getId(id);
        app.controller.game.TogglePause();
        return;
    }

}
