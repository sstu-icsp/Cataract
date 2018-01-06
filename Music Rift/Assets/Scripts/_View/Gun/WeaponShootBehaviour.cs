﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShootBehaviour : StateMachineBehaviour {

    public AudioClip shootSound;
    private AudioSource shootEfxSource;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        shootEfxSource = AudioManager.instance.PlayEffect(shootSound, true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        shootEfxSource.Stop();
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
