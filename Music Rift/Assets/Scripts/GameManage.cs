using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {

    [SerializeField]
    private GameObject GameInterface;
    [SerializeField]
    private GameObject Player;

    private Gun currentGun;

    [SerializeField]
    public Button GunButton;
    public Sprite[] images;
    public Gun[] guns;

    static public GameManage instance;

    private GameManage() { }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        guns = new Gun[] { new Gun(images[0]), new Gun(images[1]), new Gun(images[2]) };
        currentGun = new Gun(images[0]);
    }


    public Gun CurrentGun
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

    public void GameIntefaceActivate()
    {
        GameInterface.SetActive(!GameInterface.activeSelf);
    }

    public void updateHealth(int h)
    {
        Player.GetComponent<PlayerStats>().setHealth(h);
    }

    public void drawLazer(Vector3 touchPosition)
    {
        Debug.DrawLine(Player.transform.position, touchPosition,Color.red);

       // Physics2D.Linecast(trS1.position, trE1.position, 1 << LayerMask.NameToLayer("Ground"))
    }
    public void DrawLine(Vector3 start, float duration = 1f)
    {
        Color color = Color.red;
        Vector3 end = new Vector3(Player.transform.position.x, Player.transform.position.y, start.z + 2);
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, Color.blue);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

}
