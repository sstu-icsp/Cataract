﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListView : Element {

    public byte id;
    public string textList = "";
    public GameObject interfaceList;
    public GameObject list;
    private TextInfo textInfo;
    public static event Action<int> OnListCollected;

	void OnCollisionEnter2D(Collision2D other)
    {
        if (app.model.player.collectingList) return;
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            app.model.player.collectingList = true;
            
            textInfo = interfaceList.GetComponent<TextInfo>();
            textInfo.gameObject.SetActive(true);
            list.SetActive(true);
            textInfo.getId(id);
            app.controller.game.TogglePause();
            OnListCollected(id);
            return;
        }
        if(other.gameObject.tag == "Rift" && other.gameObject.GetComponent<RiftController>().PlayerDetected)
        {
            gameObject.SetActive(false);
            app.model.player.collectingList = true;
            textInfo = interfaceList.GetComponent<TextInfo>();
            textInfo.gameObject.SetActive(true);
            list.SetActive(true);
            textInfo.getId(id);
           // Debug.Log(Time.timeScale);
            app.controller.game.TogglePause();
            Debug.Log(Time.timeScale);
        }
    }

    public void ActivateInDiary()
    {
        list.SetActive(true);
    }
}
