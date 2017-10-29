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
        if (Input.GetMouseButtonDown(0) && !app.controller.game.IsPaused)//Отслеживание нажатия на экран 
        {
            startPos = app.view.player.gameObject.transform.position;
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Запись в переменную pos координат места, где произошло касание экрана.
            drawLaser();
        }
    }
    //alternative implementation
    //Vector2 heading = endPos - startPos;
    // float distance = heading.magnitude;
    // dir = heading / distance;
    //  startPos = app.view.player.gameObject.transform.position;
    //   endPos += currMode.bulletSpeed * dir;

    public void drawLaser()
    {
        Color color = Color.red;
        GameObject myLine = new GameObject();
        myLine.transform.position = startPos;
        myLine.AddComponent<LineRenderer>();
        myLine.AddComponent<EdgeCollider2D>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, Color.blue);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
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

