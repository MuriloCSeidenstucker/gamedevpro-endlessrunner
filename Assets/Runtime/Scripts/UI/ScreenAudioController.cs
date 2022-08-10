using UnityEngine;

public class ScreenAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonSFX;
    [SerializeField] private AudioClip _countdownAudio;
    [SerializeField] private AudioClip _countdownFinishedAudio;

    public void PlayButtonSFX() => AudioManager.Instance.PlaySFX(_buttonSFX);
    public void PlayCountdownAudio() => AudioManager.Instance.PlaySFX(_countdownAudio);
    public void PlayCountdownFinishedAudio() => AudioManager.Instance.PlaySFX(_countdownFinishedAudio);
}
