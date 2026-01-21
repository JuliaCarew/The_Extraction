using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : SingletonBase<AudioLibrary>
{
    [SerializeField] private List<AudioClip> sfxClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> musicClips = new List<AudioClip>();
    private Dictionary<string, AudioClip> sfxLibrary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> musicLibrary = new Dictionary<string, AudioClip>();

    private AudioSource sfxSource;
    private AudioSource musicSource;

    private float baseMusicVolume = 1f;

    [Header("Music Intensity Settings")]
    [SerializeField] private float slowSpeedMultiplier = 0.75f;
    [SerializeField] private float slowVolumeMultiplier = 0.75f;
    [SerializeField] private float normalSpeedMultiplier = 1.0f;
    [SerializeField] private float normalVolumeMultiplier = 1.0f;

    protected override void Awake()
    {
        base.Awake();
        InitializeAudioLibrary();
    }

    private void InitializeAudioLibrary()
    {
        for (int i = 0; i < sfxClips.Count; i++)
        {
            sfxLibrary.Add(sfxClips[i].name, sfxClips[i]);
        }
        for (int i = 0; i < musicClips.Count; i++)
        {
            musicLibrary.Add(musicClips[i].name, musicClips[i]);
        }
    }

    public void PlaySFX(string name)
    {
        if (sfxLibrary.ContainsKey(name))
        {
            sfxSource.PlayOneShot(sfxLibrary[name]);
        }
    }
    public void PlayMusic(string name)
    {
        if (musicLibrary.ContainsKey(name))
        {
            musicSource.PlayOneShot(musicLibrary[name]);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void SetMusicVolume(float value) // for UI sliders
    {
        baseMusicVolume = Mathf.Clamp01(value);
        musicSource.volume = baseMusicVolume;
    }

    public void SetSfxVolume(float value) // for UI sliders
    {
        sfxSource.volume = Mathf.Clamp01(value);       
    }

    public void SetMusicIntensity(float speedMultiplier, float volumeMultiplier)
    {
        if (musicSource == null) return;

        musicSource.pitch = Mathf.Clamp(speedMultiplier, 0.1f, 2.0f);
        musicSource.volume = baseMusicVolume * Mathf.Clamp01(volumeMultiplier);       
    }
}
