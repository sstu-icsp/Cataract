using System;
using UnityEngine;
using UnityEngine.UI;

public class GunView : Element
{
    public Image currentGunImage;
    public GameObject gunRenderer; 
    
    [SerializeField]
    protected GameObject modeSelectionPanel;
    private GunController controller;
    public event Action<int> OnGunChanged;

    void Awake()
    {
        controller = app.controller.gun;
        controller.SetMode(0);
        gunRenderer.gameObject.SetActive(false);
    }

    public void Shoot(Vector3 position, Vector2 touchPosition)
    {

    
    }
 
    public void SetMode(int modeInd)
    {
        controller.SetMode(modeInd);
        app.model.gunModel.CurrentGun = (sbyte)(modeInd - 1);
        ToggleModeSelectionPanel();
        OnGunChanged(modeInd);
        if (modeInd == 0)
        {
            currentGunImage.enabled = false;
            gunRenderer.SetActive(false);
        }
        else {
            currentGunImage.sprite = controller.currMode.sprite;
            currentGunImage.enabled = true;
            gunRenderer.SetActive(true);
        }
    }

  

    public void ToggleModeSelectionPanel()
    {
        if(!app.controller.game.IsPaused)modeSelectionPanel.SetActive(!modeSelectionPanel.activeSelf);
    }

}