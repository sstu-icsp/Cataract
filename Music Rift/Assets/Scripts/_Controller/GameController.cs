using UnityEngine;
using UnityEngine.UI;

public class GameController : Element
{
    public bool IsPaused { get { return isPaused; } private set { } }
    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

   //TODO: NextLevel() PauseMenu() etc
}
