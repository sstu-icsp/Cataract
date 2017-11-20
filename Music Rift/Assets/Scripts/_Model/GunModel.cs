using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModel : Element {

    [SerializeField]
    GameObject gun;
    sbyte currentGun = -1;

    public Gun[] guns = new Gun[]{ new Gun(0.1f, 0.1f), new Gun(0.1f, 0.2f) , new Gun(0.1f, 0.4f) };

    public sbyte CurrentGun
    {
        get
        {
            return currentGun;
        }

        set
        {
            currentGun = value;
        }
    }

    public GameObject GunO
    {
        get
        {
            return gun;
        }

        set
        {
            gun = value;
        }
    }

    void Awake()
    {
        guns = new Gun[]{ new Gun(0.1f, 0.1f), new Gun(0.1f, 0.2f) , new Gun(0.1f, 0.4f) };
    }
}

