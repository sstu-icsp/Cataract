using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject GameInterface;
    [SerializeField]
    private GameObject Player;
    public static GameManager instance;
    private bool pause = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void TogglePause()
    {
        GameInterface.SetActive(!GameInterface.activeSelf);
        pause = !pause;
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void updateHealth(int h)
    {
        Player.GetComponent<PlayerStats>().setHealth(h);
    }
}
