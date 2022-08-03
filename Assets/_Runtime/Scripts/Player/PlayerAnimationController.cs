using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerController _playerController;

    private void Awake() => _playerController = GetComponent<PlayerController>();

    private void LateUpdate() => _animator.SetBool(PlayerAnimationID.IsJumping, _playerController.IsJumping);
}
