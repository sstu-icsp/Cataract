using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private GameObject Health;
    [SerializeField]
    private GameObject Damage;
    [SerializeField]
    private GameObject Level;
    private int health = 5;
    private int maxHealth = 5;
    private int minHealth = 0;
    private int damage = 1;
    private int level = 0;

    public int getHealth()
    {
        return health;
    }
    public int getLevel()
    {
        return level;
    }
    public int getDamage()
    {
        return damage;
    }
	public void setHealth(int h)
    {
        health = h;
        if (health > maxHealth) health = maxHealth;
        Health.GetComponent<Text>().text = health + "/" + maxHealth;
    }
    public void setDamage(int d)
    {
        damage = d;
        Damage.GetComponent<Text>().text = "Dmg: " + damage;
    }
    public void setLevel(int l)
    {
        level = l;
        Level.GetComponent<Text>().text = "Level: " + damage;
    }

}
