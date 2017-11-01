using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindList : MonoBehaviour {

    public byte id;
    public string textList = "";
    public GameObject interfaceList;
    private TextInfo textInfo;
    
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerView>())
        {
            gameObject.SetActive(false);
            textInfo = interfaceList.GetComponent<TextInfo>();
            textInfo.gameObject.SetActive(true);
            textInfo.getId(id);
            Debug.Log(textList + " " + id);
            Time.timeScale = 0;
        }
    }
}
