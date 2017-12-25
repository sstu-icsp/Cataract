using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseEnemyController
{
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    bool facingRight = true;
    public bool grounded;
    Rigidbody2D rb;
    public float move = 1;
    byte timer = 0;
    bool playerDetected = false;

    Animator animator;

    Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        gameplay = app.controller.rhythmG;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (playerDetected)IsPlayerDetected();
        else SimpleMotion();
    }

    private void IsPlayerDetected()
    {
        animator.SetBool("Run", false);
        Debug.Log(transform.position.x + " /" + app.view.player.gameObject.transform.position.x);
        if(transform.position.x < app.view.player.gameObject.transform.position.x && move < 0 
            || transform.position.x > app.view.player.gameObject.transform.position.x && move > 0)
        {
            Flip();
        }
    }

    private void SimpleMotion()
    {
        Timer();
        if (move != 0) animator.SetBool("Run", true);
        else animator.SetBool("Run", false);
        Vector3 side = Vector3.zero;
        if (move > 0) side = Vector3.right;
        if (move < 0) side = Vector3.left;
        RaycastHit2D[] hit = Physics2D.RaycastAll(rb.position, side, 1f);
        if (hit.Length > 1)
        {
            if (hit[1].collider.tag == "Ground" && timer == 0)
            {
                Flip();
                rb.velocity = new Vector2(move, rb.velocity.y);
                timer = 100;
                return;
            }
        }
        if (((rb.position.x > startPosition.x + 1 && move > 0)
            || (rb.position.x < startPosition.x - 1 && move < 0)))
        {
            Flip();
            timer = 100;
        }
        rb.velocity = new Vector2(move, rb.velocity.y);
    }

    void Flip()
    {
        move = move * -1;
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    void Timer()
    {
        if(timer == 0)
        {
            return;
        }
        else
        {
            timer--;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerDetected = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerDetected = false;
        }
    }

    void Fire()
    {

    }
}