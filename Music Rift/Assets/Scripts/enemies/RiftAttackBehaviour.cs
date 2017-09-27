using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftAttackBehaviour : StateMachineBehaviour {

    public AudioClip attackSound;
    public RiftBehaviour behaviour;
    private Transform thisTransform;
    private Transform playerTransform;
    private AudioSource player;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player = behaviour.GetComponent<AudioSource>();
        player.clip = attackSound;
        player.Play();
        thisTransform = behaviour.GetComponentInParent<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        thisTransform.position += (playerTransform.position - thisTransform.position) / 30;
        if (!player.isPlaying)
            player.Play();
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        behaviour.GetComponent<AudioSource>().Stop();
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
