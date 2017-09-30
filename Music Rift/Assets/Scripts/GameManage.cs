using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    [SerializeField]
    private GameObject joystick;
    [SerializeField]
    private GameObject jumpButton;


    public void AxisActivate()
    {
        joystick.SetActive(!joystick.activeSelf);
        jumpButton.SetActive(!jumpButton.activeSelf);
    }
}
