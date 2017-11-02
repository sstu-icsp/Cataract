using System;
using UnityEngine;

public class RiftController : BaseEnemyController
{
    [SerializeField]
    private Animator animator;

    void Start()
    {
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Enemy");
        }
    }


    public override void Die()
    {
        Destroy(gameObject);
    }
}