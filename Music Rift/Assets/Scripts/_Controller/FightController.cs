using System;
using UnityEngine;
using UnityEngine.UI;

public class FightController : Element
{
    private PlayerController player;
    private BaseEnemyController enemy;
    private UIView view;

    void Awake()
    {
        player = app.controller.player;
        view = app.view.ui;
        EventsController.Collision += OnCollision;
        EventsController.GFinished += OnGameplayFinished;
    }

    private void OnCollision(Element p_source, object[] p_data)
    {
        if (p_source == app.controller.gun)
        {

            GameObject gameObj = (GameObject)p_data[0];
            enemy = gameObj.GetComponent<BaseEnemyController>();
            if (UnityEngine.Random.Range(0, 10) < 8)//starts combat with 80% chance
            {
                app.controller.game.TogglePause();
                view.ToggleFight();
                enemy.gameplay.Init();
            }
            else
            {
                enemy.Die();
            }
        }
    }
    //main cycle. Changes between states when state time is up. Resumes the game when enemy is dead
    void Update()
    {
        if (enemy != null)
            enemy.gameplay.UpdateGameplay();
    }


    //executed when mini-game is finished
    public void OnGameplayFinished(Element p_source, params object[] p_data)
    {
        player.ChangeHealth((int)p_data[0]);
        app.controller.game.TogglePause();
        view.ToggleFight();
        if (enemy != null)
            enemy.Die();
    }

}
