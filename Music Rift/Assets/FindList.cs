using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindList : MonoBehaviour {

    public string textList = "";
    
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerView>())
        {
            gameObject.SetActive(false);
            Debug.Log(textList);
        }
    }
}
