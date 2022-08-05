using UnityEngine;

public class JumpAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        //TODO: Assuming the PlayerController is on the parent object.
        PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
        if (player != null && clips.Length > 0)
        { 
            AnimatorClipInfo jumpClipInfo = clips[0];
            float multiplier = jumpClipInfo.clip.length / player.JumpDuration;
            animator.SetFloat(PlayerAnimationID.JumpMultiplier, multiplier);
        }
    }
}
