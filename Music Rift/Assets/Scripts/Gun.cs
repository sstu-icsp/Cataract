using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    public Sprite sprite;

    /**
     * here are the parameters of gun
     * */

    public Gun(Sprite sprite)
    {
        this.sprite = sprite;
    }
}
