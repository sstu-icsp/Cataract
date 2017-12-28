using System;
using UnityEngine;

public class PlayerView : Element {

    public Animator animator;
    public Rigidbody2D rb;
    public AudioClip attackSound; 
    public Transform trS1;
    public Transform trE1;
    public Transform trS2;
    public Transform trE2;
    private PlayerController controller;
    private PlayerModel model;
    private float jumpStartY;
    public float jumpHeight;

    void Awake()
    {
        controller = app.controller.player;
        model = app.model.player;
    }
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

	void Update () {
        animator.SetFloat("VelocityX", Math.Abs(rb.velocity.x));
        animator.SetFloat("jumpHeight", controller.jumpHeight);
        animator.SetBool("IsGrounded", model.isGrounded);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        app.controller.events.OnCollision(this, col);
    }

    public void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
