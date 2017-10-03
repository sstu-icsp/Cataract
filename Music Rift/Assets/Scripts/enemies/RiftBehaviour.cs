using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftBehaviour : Fightable
{

    public AudioClip attack_successful, attack_shielded;
    private Animator animator;
    [SerializeField]
    private GameObject fightPanel;

    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        animator.GetBehaviour<RiftIdleBehaviour>().gameObject = gameObject;
        animator.GetBehaviour<RiftAttackBehaviour>().gameObject = gameObject;
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

    public override void Attack(bool isDefended)
    {
        base.Attack(isDefended);
        if (!isDefended) {
            AudioManager.instance.PlayEffect(attack_successful);
                }
        else
        {
            AudioManager.instance.PlayEffect(attack_shielded);
        }
            
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}