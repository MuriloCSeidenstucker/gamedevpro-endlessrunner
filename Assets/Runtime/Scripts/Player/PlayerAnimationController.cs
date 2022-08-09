using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerController _playerController;

    private void Awake() => _playerController = GetComponent<PlayerController>();

    private void LateUpdate()
    {
        _animator.SetBool(PlayerAnimationID.IsJumping, _playerController.IsJumping);
        _animator.SetBool(PlayerAnimationID.IsRolling, _playerController.IsRolling);
    }

    public void OnStart() => _animator.SetTrigger(PlayerAnimationID.StartGameTrigger);
    public void OnDeath() => _animator.SetTrigger(PlayerAnimationID.DeathTrigger);
}
