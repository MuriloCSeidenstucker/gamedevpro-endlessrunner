using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerController _playerController;
    private int _animIDIsJumping;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        AssignAnimationIDs();
    }

    private void LateUpdate()
    {
        _animator.SetBool(_animIDIsJumping, _playerController.IsJumping);
    }

    private void AssignAnimationIDs()
    {
        _animIDIsJumping = Animator.StringToHash("IsJumping");
    }
}
