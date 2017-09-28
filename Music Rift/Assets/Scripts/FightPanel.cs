using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour {

    bool pause = false;
    public Button exitButton;
    [SerializeField]
    private GameObject fightPanel;

    void Awake()
    {
        exitButton.onClick.AddListener(Exit);
    }

    public void Fight(Collision2D col)
    {
        Destroy(col.gameObject);
        Exit();
    }

    public void Rift(Collision2D col)
    {   
        Destroy(col.gameObject);
        Exit();
    }

    public void Exit()
    {
        pause = !pause;
        fightPanel.SetActive(!fightPanel.activeSelf);
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
