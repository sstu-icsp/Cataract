using System;
using UnityEngine;
using UnityEngine.UI;

public class UIView : Element
{
    public Text playerHP;
    public GameObject gameInterface, fightInterface;

    public void ToggleFight()
    {
        gameInterface.SetActive(!gameInterface.activeSelf);
        fightInterface.SetActive(!fightInterface.activeSelf);
    }

    public void setHealth(int h)
    {
        playerHP.text = h.ToString();
    }

}