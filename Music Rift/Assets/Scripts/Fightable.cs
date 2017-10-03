using UnityEngine;

/// <summary>
/// Class providing basic combat logic.
/// contains useful methods Attack(), TakeDamage() and Die()
/// </summary>
public abstract class Fightable : MonoBehaviour {

    [SerializeField]
    private int health; 
    [SerializeField]
    private int damage;
    public Fightable enemy;
    public int CurrHealth { get { return currHealth; } set { } }
    private int currHealth;

    public void Start()
    {
        currHealth = health;
    }

    public virtual void Attack(bool isDefended)
    {
        if(!isDefended)
            enemy.TakeDamage(damage);
    }

    public virtual void TakeDamage(int damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
        {
            currHealth = 0;
            Die();
        }
    }

    public abstract void Die();

}
