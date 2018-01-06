using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GunView : Element
{
    [HideInInspector]
    public Image currentGunImage;
   
    public Transform leftPlayerHand;
    public GameObject gunObject;
    public LineRenderer laserLine;
    public Vector3 startPoint, endPoint;
    public Animator animator;

    [SerializeField]
    protected GameObject modeSelectionPanel;
    private GunController controller;
    public static event Action<int> OnGunChanged;

    void Awake()
    {
        controller = app.controller.gun;
        WeaponPowerUpBehaviour.OnPowerUpFinished += PowerUpFinished;
        gunObject.transform.SetParent(leftPlayerHand);
        gunObject.SetActive(false);
        laserLine = gunObject.GetComponentInChildren<LineRenderer>(true);
        animator = gunObject.GetComponent<Animator>();
    }

    void OnDestroy()
    {
        WeaponPowerUpBehaviour.OnPowerUpFinished -= PowerUpFinished;
    }
  
    public void StartShooting()
    {
        animator.ResetTrigger("PowerDown");
        animator.SetTrigger("PowerUp");
        
    }

    public void UpdateShooting()
    {

    }

    public void StopShooting()
    {
        animator.SetTrigger("PowerDown");
        animator.ResetTrigger("PowerUp");
        laserLine.gameObject.SetActive(false);
    }

    private void PowerUpFinished()
    {
        if(controller.isShooting)
            laserLine.gameObject.SetActive(true);
    }

    //TODO: GunView.SetMode() is called by buttons using inspector-defined values. If GameView is changed reassigning should be manual. What's the better way?
    public void SetMode(int modeInd)
    {
        controller.SetMode(modeInd);
        
        ToggleModeSelectionPanel();
        app.model.gunModel.CurrentGun = (sbyte)(modeInd - 1);
        OnGunChanged(modeInd);
        if (modeInd == 0)
        {
            currentGunImage.enabled = false;
            gunObject.SetActive(false);
        }
        else {
            currentGunImage.sprite = controller.currMode.sprite;
            currentGunImage.enabled = true;
            gunObject.SetActive(true);
        }
    }

    public void ToggleModeSelectionPanel()
    {
        if(!app.controller.game.IsPaused)modeSelectionPanel.SetActive(!modeSelectionPanel.activeSelf);
    }
}