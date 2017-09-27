using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToglePanelButton : MonoBehaviour {

    bool pause = false;
    public void TogglePanel(GameObject panel)
    {
        pause = !pause;
        panel.SetActive(!panel.activeSelf);
        if(pause)
        {
            Time.timeScale = 0;
        }else
        {
            Time.timeScale = 1;
        }
    }
}
