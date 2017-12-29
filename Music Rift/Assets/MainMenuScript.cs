using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("FirstScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void HelpAndSettings()
    {
        SceneManager.LoadScene("HelpAndSettingsScene");
    }
}