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

    private GunView gunView;
    Gun gun;
    private int currentModeInd;
    private Vector2 startPos, currPos, endPos, dir;
    private float timeLeft;
    float angle;
    private bool isRotated;

    void Awake()
    {
        gunView = app.view.gun;
        SetMode(0);
    }

    void Update()
    {
   
        if ((Input.GetMouseButtonDown(0) || Input.touches.Any(x => x.phase == TouchPhase.Began)) && !app.controller.game.IsPaused)//Отслеживание нажатия на экран 
        {         
            startPos = app.model.gunModel.GunO.transform.position;
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Запись в переменную pos координат места, где произошло касание экрана.
            if (!IsPointerOverUIObject())
                drawLaser();
        }
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0)
            {
                rotateGunBack();
            }
        }
    }

    public void drawLaser()
    {
        if (app.model.gunModel.CurrentGun == -1 || app.controller.game.IsPaused) return;
        FlipPlayer();
        LazerCollide();
        RotateGun();
        LazerSetup(); 
    }

    private void LazerCollide()
    {
        RaycastHit2D[] h = Physics2D.LinecastAll(startPos, endPos);
        Debug.DrawLine(startPos, endPos, Color.red);
        if (h.Length > 0)
        {
            if (h.Length > 1)
                if (h[0].collider.tag == "Rift" && !h[1].collider.isTrigger)
                {
                    app.controller.events.OnCollision(this, h[0].collider.gameObject);
                }
            if (h[0].collider.tag == "Enemy")
            {
                app.controller.events.OnCollision(this, h[0].collider.gameObject);
            }
                if (h[0].collider.tag == "Ground")
            {
                endPos = h[0].point;
            }
        }

    }

    private void LazerSetup()
    {
        gun = app.model.gunModel.guns[app.model.gunModel.CurrentGun];
        GameObject myLine = new GameObject();
        myLine.transform.position = startPos;
        myLine.AddComponent<LineRenderer>();
        myLine.GetComponent<LineRenderer>().sortingOrder = 5;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startColor = Color.cyan;
        lr.endColor = Color.blue;
        lr.startWidth = gun.startWidth;
        lr.endWidth = gun.endWidth;
        lr.SetPosition(0, new Vector3(startPos.x, startPos.y, -5));
        lr.SetPosition(1, new Vector3(endPos.x, endPos.y, -5));
        Destroy(myLine, 0.5f);
    }

    private void RotateGun()//bad work when shoot to up or down direction
    {
        startPos = app.model.gunModel.GunO.transform.position;
        rotateGunBack();
        angle = (float)(Math.Atan((endPos.y - startPos.y) / (endPos.x - startPos.x))) * Mathf.Rad2Deg;
        if (!app.model.player.facingRight) angle = -angle;
        app.model.gunModel.GunO.transform.Rotate(new Vector3(0, 0, angle));
        timeLeft = 0.5f;
        isRotated = true;
    }

    void rotateGunBack()
    {
        if (isRotated)
        {
            app.model.gunModel.GunO.transform.Rotate(new Vector3(0, 0, -angle));
            isRotated = false;
        }
    }

    private void FlipPlayer()
    {
        startPos = app.model.gunModel.GunO.transform.position;
        if(app.model.player.facingRight && app.model.player.playerObject.transform.position.x > endPos.x)
            app.controller.player.Flip();
        else if(!app.model.player.facingRight && app.model.player.playerObject.transform.position.x < endPos.x)
            app.controller.player.Flip();
    }

    public void SetMode(int currentModeInd)
    {
        this.currentModeInd = currentModeInd;
        currMode = modes[currentModeInd];
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