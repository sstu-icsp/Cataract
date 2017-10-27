using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunButton : MonoBehaviour {

    [SerializeField]
    protected GameObject ChoiceGunPanel;

    virtual public void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ChoiceGun);
    }

    virtual protected void ChoiceGun()
    {
        ChoiceGunPanel.SetActive(!ChoiceGunPanel.activeSelf);
        GameManager.instance.ChooseGun(gameObject.GetComponent<Button>().image.sprite);
    }
}
