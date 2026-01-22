using UnityEngine;
using System;

public class AudioManager : SingletonBase<AudioManager>
{
    [Header("Data")]
    [SerializeField] private AudioLibrary library;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music Intensity Settings")]
    [SerializeField] private float slowSpeedMultiplier = 0.75f;
    [SerializeField] private float slowVolumeMultiplier = 0.75f;
    [SerializeField] private float normalSpeedMultiplier = 1.0f;
    [SerializeField] private float normalVolumeMultiplier = 1.0f;

    private float baseMusicVolume = 1.0f;

    protected override void Awake()
    {
        base.Awake();

        // Store base volume for intensity changes
        baseMusicVolume = musicSource.volume;
    }


    public void PlayMusic(string id)
    {
        AudioClip clip = library.GetMusicClip(id);
        if (clip == null) return;

        musicSource.loop = true;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySound(string id)
    {
        AudioClip clip = library.GetSFXClip(id);
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
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

    // adjusts speed and volume of music for intensity changes
    public void SetMusicIntensity(float speedMultiplier, float volumeMultiplier)
    {
        if (musicSource == null) return;

        musicSource.pitch = Mathf.Clamp(speedMultiplier, 0.1f, 2.0f);
        musicSource.volume = baseMusicVolume * Mathf.Clamp01(volumeMultiplier);
    }

    // called when player picks up a weapon, sets music to normal intensity
    public void OnWeaponPickedUp()
    {
        SetMusicIntensity(normalSpeedMultiplier, normalVolumeMultiplier);
    }
}