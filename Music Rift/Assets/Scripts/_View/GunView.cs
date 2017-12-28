using System;
using UnityEngine;
using UnityEngine.UI;

public class GunView : Element
{
    public Image currentGunImage;
    public SpriteRenderer gunRenderer; 
    
    [SerializeField]
    protected GameObject modeSelectionPanel;
    private GunController controller;
    public event Action<int> OnGunChanged;

    void Awake()
    {
        controller = app.controller.gun;
        controller.SetMode(0);
        gunRenderer.sprite = currentGunImage.sprite = controller.currMode.sprite;
    }

    public void Shoot(Vector3 position, Vector2 touchPosition)
    {

    
    }
 
    public void SetMode(int modeInd)
    {
        controller.SetMode(modeInd);
        app.model.gunModel.CurrentGun = (sbyte)(modeInd - 1);
        ToggleModeSelectionPanel();
        gunRenderer.sprite = currentGunImage.sprite = controller.currMode.sprite;
        OnGunChanged(modeInd);
        if (modeInd == 0)
            currentGunImage.enabled = false;
        else
            currentGunImage.enabled = true;
    }

  

    public void ToggleModeSelectionPanel()
    {
        if(!app.controller.game.IsPaused)modeSelectionPanel.SetActive(!modeSelectionPanel.activeSelf);
    }

}