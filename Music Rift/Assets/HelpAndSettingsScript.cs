using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpAndSettingsScript : MonoBehaviour
{
    public GameObject SettingsPanel;
    private bool ToggleMusic = true;
    private bool ToggleJoystick = true;

    void Start()
    {
        setBoolToggle("ToggleMusic", ref ToggleMusic);
        setBoolToggle("ToggleJoystick", ref ToggleJoystick);
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Save()
    {
        if (ToggleMusic)
            PlayerPrefs.SetFloat("ToggleMusic", 1);
        else PlayerPrefs.SetFloat("ToggleMusic", 0);
        if(ToggleJoystick)
            PlayerPrefs.SetFloat("ToggleJoystick", 1);
        else PlayerPrefs.SetFloat("ToggleJoystick", 0);
    }

    public void MusicToggleChanged(bool isOn)
    {
        ToggleMusic = !ToggleMusic;
    }
    public void JoystickToggleChanged(bool isOn)
    {
        ToggleJoystick = !ToggleJoystick;
    }

    void setBoolToggle(string name, ref bool variable)
    {
        Toggle[] toggles = SettingsPanel.GetComponentsInChildren<Toggle>();
        Toggle t = null;
        foreach (Toggle item in toggles)
        {
            if (item.gameObject.name == name)
            {
                t = item;
                Debug.Log(item.gameObject.name);
                break;
            }
        }
        if(t != null)
        if (PlayerPrefs.HasKey(name))
        {
            if (PlayerPrefs.GetFloat(name) != 0)
                t.isOn = variable;
            else t.isOn = false;
        }
        else
        {
            t.isOn = variable;
        }
    }
}
