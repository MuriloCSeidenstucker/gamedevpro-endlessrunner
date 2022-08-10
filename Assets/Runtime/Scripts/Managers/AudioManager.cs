using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    protected override void Awake()
    {
        base.Awake();

        _musicSource.loop = true;
        _sfxSource.loop = false;
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void StopMusic() => _musicSource.Stop();
}
