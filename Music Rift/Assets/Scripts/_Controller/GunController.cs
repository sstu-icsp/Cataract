using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunController : Element
{
    [SerializeField]
    private GunMode[] modes;
    [SerializeField]
    public GunMode currMode;
    public static event Action OnPowerUp, OnStopShooting;
    public float gunRotationSpeed, shootingGunRotationSpeed;
    public bool isShooting;

    private GunView view;
    private Vector3 laserStartPos, touchPos, laserEndPos, gunToTouchDir;
    readonly private float MIN_ANGLE = -45, MAX_ANGLE = 45;
    readonly private float LASER_LENGTH = 15;
    private float laserLength;
    private int currentModeInd;


    void Awake()
    {
        view = app.view.gun;
        SetMode(0);
    }

    void Update()
    {
        RotateGunToMousePosition();
        if ((Input.GetMouseButtonDown(0) && currentModeInd != 0 && !IsPointerOverUIObject() && !app.controller.game.IsPaused))
        {
            UpdateLaser();
            RaycastLaser(false);
            view.StartShooting();
            OnPowerUp();
            isShooting = true;
        }
        else if (Input.GetMouseButton(0) && isShooting)
        {
            UpdateLaser();
            RaycastLaser(true);
            view.UpdateShooting();
        }
        if (Input.GetMouseButtonUp(0) && isShooting)
        {
            view.StopShooting();
            OnStopShooting();
            isShooting = false;
        }
    }

    private void UpdateLaser()
    {
        Vector3 gunRotation = view.gunObject.transform.right.normalized;
        if (!app.model.player.facingRight)
            gunRotation = -gunRotation;
        laserEndPos = new Vector3(laserStartPos.x + gunRotation.x * laserLength, laserStartPos.y + gunRotation.y * laserLength, -2);
        view.laserLine.SetPosition(0, laserStartPos);
        view.laserLine.SetPosition(1, laserEndPos);
    }

    private void RotateGunToMousePosition()
    {
        touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Запись координат места, где произошло касание экрана.
        touchPos.z = 0;
        laserStartPos = view.laserLine.transform.position;
        gunToTouchDir = touchPos - laserStartPos;
        if (!app.model.player.facingRight)
            gunToTouchDir = -gunToTouchDir;
        float angle = (float)(Math.Atan(gunToTouchDir.y / gunToTouchDir.x) * Mathf.Rad2Deg);
        if (gunToTouchDir.x < 0) angle = -angle;
        angle = Mathf.Clamp(angle, MIN_ANGLE, MAX_ANGLE);
        float speed;
        if (isShooting)
            speed = Time.deltaTime * shootingGunRotationSpeed;
        else
            speed = Time.deltaTime * gunRotationSpeed;
        view.gunObject.transform.rotation = Quaternion.Slerp(view.gunObject.transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), speed);
    }

    private void RaycastLaser(bool checkCollisions)
    {
        laserLength = LASER_LENGTH;
        RaycastHit2D[] h = Physics2D.RaycastAll(laserStartPos, gunToTouchDir);
        foreach (RaycastHit2D hit in h)
        {
            if (hit.collider.tag == "Rift" || hit.collider.tag == "Ground" || hit.collider.tag == "Enemy")
            {
                laserLength = (hit.point - (Vector2)laserStartPos).magnitude;
                break;
            }
        }
        if (checkCollisions && h.Length > 0)
        {
            if (h[0].collider.tag == "Enemy")
            {
                app.controller.events.OnCollision(this, h[0].collider.gameObject);
            }
            else if (h.Length > 1)
            {
                if (h[0].collider.tag == "Rift" && !h[1].collider.isTrigger)
                {
                    app.controller.events.OnCollision(this, h[0].collider.gameObject);
                }
            }
        }
    }

    public void SetMode(int currentModeInd)
    {
        currMode = modes[currentModeInd];
        this.currentModeInd = currentModeInd;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}