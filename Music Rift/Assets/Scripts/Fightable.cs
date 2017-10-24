using UnityEngine;

/// <summary>
/// Class providing basic combat logic:
/// Health, Gameplay, damage
/// ChangeHealth() and Die()
/// </summary>
public abstract class Fightable : MonoBehaviour
{

    public Fightable enemy;
    public FightGameplay Gameplay { get; set; }
    public int CurrHealth { get; private set; }
    public int Health { get; private set; }

    private FightGameplay gameplay;
    [SerializeField]
    private int health;
    [SerializeField]
    private int damage;
    private int currHealth;

    public void Start()
    {
        currHealth = health;
    }

    public virtual void ChangeHealth(int val)
    {
        currHealth -= val;
        if (currHealth <= 0)
        {
            currHealth = 0;
            Die();
        }
        else if (currHealth > health)
            currHealth = health;
    }

    public abstract void Die();

}
