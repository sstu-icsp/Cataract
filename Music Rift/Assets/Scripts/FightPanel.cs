﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour {

    public Button exitButton;
    public Fightable player;
    public Button attackButton, defenceButton, ignoreButton;
    [SerializeField]
    private GameObject fightPanel;
    [SerializeField]
    private GameObject gameManager;
    private bool pause = false;
    private Fightable enemy;
    private state currState;
    private float timeRemaining;
    private const float stateTime = 5;
    private bool isDefended;
    private PlayerStats playerStats;
    public Text playerHP;
    public Text enemyHP;
    public Text playerLevel;
    public Text enemyLevel;
    public Text playerDmg;
    public Text enemyDmg;

    void Awake()
    {
        exitButton.onClick.AddListener(TogglePause);
        attackButton.onClick.AddListener(Attack);
        defenceButton.onClick.AddListener(Defend);
        ignoreButton.onClick.AddListener(Ignore);
    }

    //main cycle. Changes between states when state time is up. Resumes the game when enemy is dead
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
            ChangeState();
        if (enemy == null) TogglePause();
    }

    //executed ONCE when the fight starts
    public void Fight(Fightable enemy)
    {
        //initializing logic
        TogglePause();
        player.enemy = enemy;
        enemy.enemy = player;
        this.enemy = enemy;
        currState = (state)(UnityEngine.Random.Range(0, 2));
        if (currState == state.attack)
        {
            attackButton.gameObject.SetActive(true);
            defenceButton.gameObject.SetActive(false);   
        }
        else
        {
            attackButton.gameObject.SetActive(false);
            defenceButton.gameObject.SetActive(true);
        }
        timeRemaining = stateTime;

        //initializing GUI
        playerHP.text = "Player: " + player.CurrHealth + " HP";
        enemyHP.text = "Enemy: " + enemy.CurrHealth + " HP";  
    }

    private void ChangeState()
    {
        timeRemaining = stateTime;
        if(currState == state.attack)
        {
            currState = state.defence;
            isDefended = false;
            attackButton.gameObject.SetActive(false);
            defenceButton.gameObject.SetActive(true);
        }
        else
        {
            currState = state.attack;
            attackButton.gameObject.SetActive(true);
            defenceButton.gameObject.SetActive(false);
            enemy.Attack(isDefended);
        }
        gameManager.GetComponent<GameManage>().updateHealth(player.CurrHealth);
        Debug.Log("Player: " + player.CurrHealth + " HP");
        Debug.Log("Enemy: " + enemy.CurrHealth + " HP");
        playerHP.text = "Player: " + player.CurrHealth + " HP";
        enemyHP.text = "Enemy: " + enemy.CurrHealth + " HP";
    }

    //executed on player attack
    public void Attack()
    {
        player.Attack(false);
        ChangeState();
    }

    //executed on player defence
    public void Defend()
    {
        isDefended = true;
        ChangeState();
    }

    //executed on player ignore
    public void Ignore()
    {
        ChangeState();
    }

    public void TogglePause()
    {
        pause = !pause;
        fightPanel.SetActive(!fightPanel.activeSelf);
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            gameManager.GetComponent<GameManage>().GameIntefaceActivate();
            Time.timeScale = 1;
        }
    }
    private enum state { attack, defence };
}
