using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSFX;
    [SerializeField] private AudioClip _rollSFX;

    public void PlayJumpSFX() => AudioManager.Instance.PlaySFX(_jumpSFX);
    public void PlayRollSFX() => AudioManager.Instance.PlaySFX(_rollSFX);
}
