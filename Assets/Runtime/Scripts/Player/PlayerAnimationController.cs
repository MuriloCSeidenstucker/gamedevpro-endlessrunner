using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerController _playerController;

    private const string StartRun = "StartRun";

    private void Awake() => _playerController = GetComponent<PlayerController>();

    private void LateUpdate()
    {
        _animator.SetBool(PlayerAnimationID.IsJumping, _playerController.IsJumping);
        _animator.SetBool(PlayerAnimationID.IsRolling, _playerController.IsRolling);
    }

    public IEnumerator OnStartCor()
    {
        _animator.SetTrigger(PlayerAnimationID.StartGameTrigger);

        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(StartRun))
        {
            yield return null;
        }

        while (_animator.GetCurrentAnimatorStateInfo(0).IsName(StartRun)
            && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
    }

    public void OnDeath() => _animator.SetTrigger(PlayerAnimationID.DeathTrigger);
}
