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
    public void PlaySFXOneShot(AudioClip clip)
    {
        if (_sfxSource.outputAudioMixerGroup == null)
        {
            Debug.LogError("MissingComponentError: There is no \"AudioMixerGroup\" attached to the \"AudioSource\" game object.");
        }
        else
        {
            _sfxSource.PlayOneShot(clip);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (_sfxSource.outputAudioMixerGroup == null)
        {
            Debug.LogError("MissingComponentError: There is no \"AudioMixerGroup\" attached to the \"AudioSource\" game object.");
        }
        else
        {
            _sfxSource.clip = clip;
            _sfxSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.outputAudioMixerGroup == null)
        {
            Debug.LogError("MissingComponentError: There is no \"AudioMixerGroup\" attached to the \"AudioSource\" game object.");
        }
        else
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }
    }

    public void StopMusic() => _musicSource.Stop();
}
