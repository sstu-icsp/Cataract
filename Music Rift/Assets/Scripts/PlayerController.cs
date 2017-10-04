using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CnControls;
using System;

public class PlayerController : Fightable
{
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    public AudioClip attackSound;
    bool facingRight = true;
    public bool grounded;
    Rigidbody2D rb;
    public float move;

    public Transform trS1;
    public Transform trE1;
    public Transform trS2;
    public Transform trE2;
    [SerializeField]
    private GameObject fightPanel;
    [SerializeField]
    private GameObject gameManager;

    new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
       // move = Input.GetAxis("Horizontal");
        move = CnInputManager.GetAxis("Horizontal");

        grounded = Physics2D.Linecast(trS1.position, trE1.position, 1 << LayerMask.NameToLayer("Ground")) 
            || Physics2D.Linecast(trS2.position, trE2.position, 1 << LayerMask.NameToLayer("Ground"));
       /* Debug.DrawLine(trS1.position, trE1.position, Color.green);
        Debug.DrawLine(trS2.position, trE2.position, Color.green);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x + 0.8f, transform.position.y + 0.5f), Color.green);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x - 0.8f, transform.position.y - 0.5f), Color.green);*/

        if(CnInputManager.GetButton("Jump") && grounded)
        {
            rb.AddForce(new Vector2(0f, 700));
            rb.velocity = new Vector2(0, 0);
        }
        /* if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
         {
             rb.AddForce(new Vector2(0f, 700));
         }*/
       /* if (wallCollision())
        {
            move = 0;
        }*/
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        if (move > 0 && !facingRight)Flip();
        else if (move < 0 && facingRight)Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    bool wallCollision()//check walls near with player
    {
        return Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(transform.position.x + 0.8f, transform.position.y + 0.5f), 1 << LayerMask.NameToLayer("Ground")) && move > 0
            || Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(transform.position.x - 0.8f, transform.position.y + 0.5f), 1 << LayerMask.NameToLayer("Ground")) && move < 0
            || Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(transform.position.x + 0.8f, transform.position.y - 0.5f), 1 << LayerMask.NameToLayer("Ground")) && move > 0
            || Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(transform.position.x - 0.8f, transform.position.y - 0.5f), 1 << LayerMask.NameToLayer("Ground")) && move < 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Enemy": fightPanel.GetComponent<FightPanel>().Fight(col.gameObject.GetComponent<Fightable>());
                gameManager.GetComponent<GameManage>().GameIntefaceActivate(); break;
            case "Exit" : Application.LoadLevel(Application.loadedLevel);break;
            case "Acid" : Application.LoadLevel(Application.loadedLevel); break;
        }

    }

    public override void Attack(bool isDefended)
    {
        base.Attack(false);
        AudioManager.instance.PlayEffect(attackSound);
    }

    public override void Die()
    {
        if (fightPanel.gameObject.active)
            fightPanel.GetComponent<FightPanel>().TogglePause();
        Application.LoadLevel(Application.loadedLevel);
    }
}
