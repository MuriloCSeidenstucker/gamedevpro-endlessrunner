using UnityEngine;

public class WaitGameStartAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO: Assuming the PlayerController is on the parent object.
        PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
        player.enabled = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (InputManager.Instance.GetStartRunInput())
        {
            animator.SetTrigger(PlayerAnimationID.StartGameTrigger);
        }
    }
}
