using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindList : Element {

    public byte id;
    public string textList = "";
    public GameObject interfaceList;
    private TextInfo textInfo;
    
	void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("is");
        if (other.gameObject.GetComponent<PlayerView>())
        {
            gameObject.SetActive(false);
            textInfo = interfaceList.GetComponent<TextInfo>();
            textInfo.gameObject.SetActive(true);
            textInfo.getId(id);
            Debug.Log(textList + " " + id);
            app.controller.game.TogglePause();
           // Time.timeScale = 0;
        }
    }
}
