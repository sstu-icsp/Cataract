using System.Linq;
using UnityEngine;

public class GunController : Element
{
    [SerializeField]
    private GunMode[] modes;
    public GunMode currMode;

    private GunView view;
    private int currentModeInd;
    private Vector2 startPos, currPos, endPos, dir;

    void Awake()
    {
        view = app.view.gun;
        SetMode(0);
    }

    void Update()
    {
   
        if ((Input.GetMouseButtonDown(0) || Input.touches.Any(x => x.phase == TouchPhase.Began)) && !app.controller.game.IsPaused)//Отслеживание нажатия на экран 
        {         
            startPos = app.view.player.gameObject.transform.position;
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Запись в переменную pos координат места, где произошло касание экрана.
            drawLaser();
        }
    }

    public void drawLaser()
    {
        if (app.model.gunModel.CurrentGun == -1) return;
        Gun gun = app.model.gunModel.guns[app.model.gunModel.CurrentGun];
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
        RaycastHit2D hit = Physics2D.Raycast(startPos, endPos, Mathf.Sqrt(Mathf.Pow(endPos.x - startPos.x, 2) +
           Mathf.Pow(endPos.y - startPos.y, 2)), 1 << LayerMask.NameToLayer("Enemy"));
        if (hit.collider != null)
        {
            app.controller.events.OnCollision(this, hit.collider.gameObject);
        }
        Destroy(myLine, 1f);
    }

    public void SetMode(int currentModeInd)
    {
        this.currentModeInd = currentModeInd;
        currMode = modes[currentModeInd];
    }

}