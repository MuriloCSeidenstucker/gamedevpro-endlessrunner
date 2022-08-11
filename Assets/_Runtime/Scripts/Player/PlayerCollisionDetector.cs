using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(PlayerAnimationController))]
public class PlayerCollisionDetector : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerAnimationController _animationController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _animationController = GetComponent<PlayerAnimationController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Obstacle>(out var obstacle))
        {
            _playerController.OnDeath();
            _animationController.OnDeath();
            obstacle.PlayCollisionFeedback(other);
            GameManager.Instance.OnGameOver();
        }

        if (other.TryGetComponent<Pickup>(out var pickup))
        {
            pickup.OnPickedUp();
        }
    }
}
