using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftBehaviour : MonoBehaviour
{

    private Animator animator;
    [SerializeField]
    private GameObject fightPanel;

    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        animator.GetBehaviour<RiftIdleBehaviour>().behaviour = this;
        animator.GetBehaviour<RiftAttackBehaviour>().behaviour = this;
        Debug.Log("Awake");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Triggered");
            animator.SetBool("isPlayerDetected", true);
        }
    }

    void OnCollider2DEnter(Collider2D col)
    {
        fightPanel.GetComponent<FightPanel>().Rift(gameObject.GetComponent<Collision2D>());
    }
    void OnCollisionEnter2D(Collision2D col)
    {
       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isPlayerDetected", false);
        }
    }
}