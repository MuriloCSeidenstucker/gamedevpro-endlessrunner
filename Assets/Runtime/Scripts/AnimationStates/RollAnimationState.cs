using UnityEngine;

public class RollAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
        if (player != null && clips.Length > 0)
        {
            AnimatorClipInfo rollClipInfo = clips[0];
            float multiplier = rollClipInfo.clip.length / player.RollDuration;
            animator.SetFloat(PlayerAnimationID.RollMultiplier, multiplier);
        }
    }
}
