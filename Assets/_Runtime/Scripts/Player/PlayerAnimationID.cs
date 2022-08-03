using UnityEngine;

public static class PlayerAnimationID
{
    public static int IsJumping = Animator.StringToHash("IsJumping");
    public static int JumpMultiplier = Animator.StringToHash("JumpMultiplier");
    public static int StartGameTrigger = Animator.StringToHash("StartGameTrigger");
    public static int DeathTrigger = Animator.StringToHash("DeathTrigger");
}
