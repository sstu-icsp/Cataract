using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    [SerializeField]
    public Button GunButton;
    public Sprite[] images;
    public Gun[] guns;
    public bool IsPaused { get { return isPaused; } private set { } }

    [SerializeField]
    private GameObject fightPanel;
    [SerializeField]
    private GameObject GameInterface;
    [SerializeField]
    private GameObject Player;
    private bool isPaused = false;
    private Gun currentGun;

    public GameObject FightPanel
    {
        get
        {
            return fightPanel;
        }

        set
        {
            fightPanel = value;
        }
    }

    private GameManager() { }

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


    public void drawLazer(Vector3 end, float duration = 1f)
    {
        Color color = Color.red;
        Vector3 start = Player.transform.position;
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        end = new Vector3(end.x, end.y, Player.transform.position.z);
        myLine.AddComponent<LineRenderer>();
        myLine.AddComponent<EdgeCollider2D>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, Color.blue);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        RaycastHit2D hit = Physics2D.Raycast(start, end, Mathf.Sqrt(Mathf.Pow(end.x - start.x, 2) +
           Mathf.Pow(end.y - start.y, 2) + Mathf.Pow(end.z - start.z, 2)), 1 << LayerMask.NameToLayer("Enemy"));
        if (hit.collider != null)
        {
            FightPanel.GetComponent<FightPanel>().Fight(hit.collider.gameObject.GetComponent<Fightable>());
        }
        Destroy(myLine, duration);
    }

    public void ChooseGun(Sprite sprite)
    {
        currentGun.sprite = sprite;
        GunButton.gameObject.GetComponent<Image>().sprite = currentGun.sprite;
    }


    public void TogglePause()
    {
        GameInterface.SetActive(!GameInterface.activeSelf);
        isPaused = !isPaused;
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void updateHealth(int h)
    {
        Player.GetComponent<PlayerStats>().setHealth(h);
    }
}
