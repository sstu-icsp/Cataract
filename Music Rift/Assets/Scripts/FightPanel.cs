using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{

    public Fightable player;
    public Text playerHP, enemyHP;
    public Text playerLevel, enemyLevel;
    public Text playerDmg, enemyDmg;
    [SerializeField]
    private GameObject fightPanel;
    private Fightable enemy;
    private PlayerStats playerStats;

    //main cycle. Changes between states when state time is up. Resumes the game when enemy is dead
    void Update()
    {
        enemy.Gameplay.Update();
    }

    //executed ONCE when the fight starts
    public void Fight(Fightable enemy)
    {
        if (UnityEngine.Random.Range(0, 10) < 8)//starts combat with 80% chance
        {
            //initializing logic
            GameManager.instance.TogglePause();
            fightPanel.SetActive(!fightPanel.activeSelf);
            player.enemy = enemy;
            enemy.enemy = player;
            this.enemy = enemy;
            FightGameplay.OnGFinished += OnGameplayFinished;
            enemy.Gameplay.Init();

            //initializing GUI
            playerHP.text = "Player: " + player.CurrHealth + " HP";
            enemyHP.text = "Enemy: " + enemy.CurrHealth + " HP";
        }
        else
        {
            enemy.Die();
        }
    }

    private void ChangeState()
    {
        enemy.Gameplay.Init();
        GameManager.instance.updateHealth(player.CurrHealth);
        Debug.Log("Player: " + player.CurrHealth + " HP");
        Debug.Log("Enemy: " + enemy.CurrHealth + " HP");
        playerHP.text = "Player: " + player.CurrHealth + " HP";
        enemyHP.text = "Enemy: " + enemy.CurrHealth + " HP";
    }

    //executed when mini-game is finished
    public void OnGameplayFinished(int result)
    {
        player.ChangeHealth(result);
        GameManager.instance.updateHealth(player.CurrHealth);
        FightGameplay.OnGFinished -= OnGameplayFinished;
        GameManager.instance.TogglePause();
        fightPanel.SetActive(!fightPanel.activeSelf);
        enemy.Die();
    }
 
    private enum state { attack, defence };
}
