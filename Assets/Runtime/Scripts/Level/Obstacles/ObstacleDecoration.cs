using UnityEngine;

public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] private AudioClip _collisionSFX;
    [SerializeField] private Animation _collisionAnimation;

    public void PlayCollisionFeedback()
    {
        AudioManager.Instance.PlaySFX(_collisionSFX);
        _collisionAnimation?.Play();
    }
}
