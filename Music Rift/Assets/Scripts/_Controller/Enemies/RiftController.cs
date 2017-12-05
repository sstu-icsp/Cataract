using System;
using UnityEngine;

public class RiftController : BaseEnemyController
{

    public float maxSpeed;
    public float reactionSpeed;
    public float hitForce;
    public float throwBackForce;
    public AudioClip hitSound;
    public bool PlayerDetected = false;

    [SerializeField]
    private Animator animator;
    private Rigidbody2D rb;
    private Rigidbody2D rbPlayer;
    private bool isHit;
    private float hitTime;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rbPlayer = app.view.player.rb;
        animator.GetBehaviour<RiftIdleBehaviour>().gameObject = gameObject;
        RiftAttackBehaviour attack = animator.GetBehaviour<RiftAttackBehaviour>();
        attack.controller = this;
        gameplay = app.controller.rhythmG;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isPlayerDetected", true);
            PlayerDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isPlayerDetected", false);
            PlayerDetected = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Vector2 dirPlayerToRift = (rb.position - rbPlayer.position).normalized;
            rb.velocity = dirPlayerToRift * throwBackForce;
            rbPlayer.velocity = -dirPlayerToRift * hitForce;
            app.controller.player.ChangeHealth(-10);
            AudioManager.instance.PlayEffect(hitSound);
            isHit = true;
            hitTime = 2;
        }
    }

    public void UpdateAttack()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += ((rbPlayer.position - rb.position).normalized * reactionSpeed);
        }
        else if (rb.velocity.magnitude > maxSpeed)
        {
            if (!isHit)
                rb.velocity = (rbPlayer.position - rb.position).normalized * maxSpeed;
            else
            {
                hitTime -= Time.unscaledDeltaTime;
                if (hitTime <= 0)
                    isHit = false;
            }
        }
    }


    public override void Die()
    {
        Destroy(gameObject);
    }
}