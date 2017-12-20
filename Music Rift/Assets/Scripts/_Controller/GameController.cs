using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Element
{
    public bool IsPaused { get { return isPaused; } private set { } }
    private bool isPaused = false;
    public GameObject joystick;
    public GameObject jumpButton;

    void Start()
    {
        if (PlayerPrefs.HasKey("ToggleJoystick"))
        {
            if(PlayerPrefs.GetFloat("ToggleJoystick") == 0)
            {
                joystick.SetActive(false);
                jumpButton.SetActive(false);
            }else
            {
                joystick.SetActive(true);
                jumpButton.SetActive(true);
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //TODO: NextLevel() PauseMenu() etc
}
