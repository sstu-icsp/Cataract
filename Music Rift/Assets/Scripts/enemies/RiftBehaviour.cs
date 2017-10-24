using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftBehaviour : Fightable
{

    public AudioClip attack_successful, attack_shielded;
    private Animator animator;


    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        animator.GetBehaviour<RiftIdleBehaviour>().gameObject = gameObject;
        animator.GetBehaviour<RiftAttackBehaviour>().gameObject = gameObject;
        Gameplay = new RhythmGameplay();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isPlayerDetected", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isPlayerDetected", false);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}