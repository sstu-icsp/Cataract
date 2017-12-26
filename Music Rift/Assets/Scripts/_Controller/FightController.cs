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

    public bool IsFighting
    {
        get
        {
            return isFighting;
        }

        set
        {
            isFighting = value;
        }
    }

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
        IsFighting = true;
        enemy.gameplay.Init();       
    }

    public void EndFight()
    {
        if (enemy != null)
            enemy.Die();
    }

    //main cycle. Changes between states when state time is up. Resumes the game when enemy is dead
    void Update()
    {
        if (IsFighting)
            enemy.gameplay.UpdateGameplay();
    }


    //executed when mini-game is finished
    public void OnGameplayFinished(Element p_source, params object[] p_data)
    {
        player.ChangeHealth((int)p_data[0]);
        view.AnimateFightEnd();
        IsFighting = false;    
    }

}
