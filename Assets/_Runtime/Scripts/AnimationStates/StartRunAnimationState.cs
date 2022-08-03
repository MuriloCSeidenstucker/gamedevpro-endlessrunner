using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRunAnimationState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO: Assuming the PlayerController is on the parent object.
        PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
        player.enabled = true;
    }
}
