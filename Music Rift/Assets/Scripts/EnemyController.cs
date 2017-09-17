﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{

    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    bool facingRight = true;
    public bool grounded;
    Rigidbody2D rb;
    public float move = 1;

    Vector3 startPosition;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
	}

    void FixedUpdate()
    {
        if (rb.position.x > startPosition.x + 2 || rb.position.x < startPosition.x - 2)
        {
            move = -move;
            Flip();
        }
        rb.velocity = new Vector2(move , rb.velocity.y);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
