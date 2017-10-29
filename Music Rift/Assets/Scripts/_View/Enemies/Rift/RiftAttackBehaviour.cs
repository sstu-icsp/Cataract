using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftAttackBehaviour : StateMachineBehaviour {

    public GameObject gameObject;
    public AudioClip attackSound;
    private Transform thisTransform;
    private Transform playerTransform;
    private AudioSource player;
    private Collider2D collider2D;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player = gameObject.GetComponent<AudioSource>();
        player.clip = attackSound;
       // player.Play();
        thisTransform = gameObject.GetComponentInParent<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        collider2D = gameObject.GetComponentInParent<Collider2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       thisTransform.position += (playerTransform.position - thisTransform.position) / 200;
        collider2D.transform.position += (playerTransform.position - thisTransform.position) / 200;
        if (!player.isPlaying)
            player.Play();
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        gameObject.GetComponent<AudioSource>().Stop();
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
