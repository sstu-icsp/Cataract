using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftBehaviour : MonoBehaviour
{

    private Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        animator.GetBehaviour<RiftIdleBehaviour>().behaviour = this;
        animator.GetBehaviour<RiftAttackBehaviour>().behaviour = this;
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
}