using UnityEngine;

public static class PlayerAnimationID
{
    public static int IsJumping = Animator.StringToHash("IsJumping");
    public static int IsRolling = Animator.StringToHash("IsRolling");
    public static int JumpMultiplier = Animator.StringToHash("JumpMultiplier");
    public static int RollMultiplier = Animator.StringToHash("RollMultiplier");
    public static int StartGameTrigger = Animator.StringToHash("StartGameTrigger");
    public static int DeathTrigger = Animator.StringToHash("DeathTrigger");
}
