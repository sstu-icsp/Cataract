using System;
using UnityEngine;

public class WeaponPowerUpBehaviour : StateMachineBehaviour
{

    public AudioClip powerUpSound;
    private AudioSource source;
    public static event Action OnPowerUpFinished;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source = AudioManager.instance.PlayEffect(powerUpSound);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source.Stop();
        OnPowerUpFinished();
    }

}
