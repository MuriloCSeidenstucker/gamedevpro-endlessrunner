using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _mainTrackMusic;
    [SerializeField] private AudioClip _startMenuMusic;
    [SerializeField] private AudioClip _gameOverMusic;

    public void PlayMainTrackMusic() => AudioManager.Instance.PlayMusic(_mainTrackMusic);
    public void PlayStartMenuMusic() => AudioManager.Instance.PlayMusic(_startMenuMusic);
    public void PlayGameOverMusic() => AudioManager.Instance.PlayMusic(_gameOverMusic);
}
