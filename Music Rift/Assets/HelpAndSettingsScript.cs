using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpAndSettingsScript : MonoBehaviour
{
    public GameObject SettingsPanel;
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            if (PlayerPrefs.GetFloat("Volume") != 0)
            SettingsPanel.GetComponentInChildren<Toggle>().isOn = true;
            else SettingsPanel.GetComponentInChildren<Toggle>().isOn = false;
        }
        else
        {
            SettingsPanel.GetComponentInChildren<Toggle>().isOn = true;
        }
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Save()
    {
        if(SettingsPanel.GetComponentInChildren<Toggle>().isOn)
            PlayerPrefs.SetFloat("Volume", 1);
        else PlayerPrefs.SetFloat("Volume", 0);

    }
}
