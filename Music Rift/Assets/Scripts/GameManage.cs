using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {

    [SerializeField]
    private GameObject GameInterface;
    [SerializeField]
    private GameObject Player;



    public void GameIntefaceActivate()
    {
        GameInterface.SetActive(!GameInterface.activeSelf);
    }

    public void updateHealth(int h)
    {
        Player.GetComponent<PlayerStats>().setHealth(h);
    }
}
