using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FightController : Element
{
    private PlayerController player;
    private BaseEnemyController enemy;
    private UIView view;
    private bool isFighting;

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
                //app.controller.game.TogglePause();
                view.AnimateFightStart();
            }
            else
            {
                enemy.Die();
            }
        }
    }

    public void StartFight()
    {
        enemy.gameplay.Init();
        isFighting = true;
    }

    public void EndFight()
    {
        if (enemy != null)
            enemy.Die();
    }

    //main cycle. Changes between states when state time is up. Resumes the game when enemy is dead
    void Update()
    {
        if (isFighting)
            enemy.gameplay.UpdateGameplay();
    }


    //executed when mini-game is finished
    public void OnGameplayFinished(Element p_source, params object[] p_data)
    {
        player.ChangeHealth((int)p_data[0]);
        view.AnimateFightEnd();
        isFighting = false;    
    }

}
