using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>, IAudioService
{
    [Header("Data")]
    [SerializeField] private AudioData library;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private AudioPlayer musicPlayer;
    private AudioPlayer sfxPlayer;
    public bool debugMode = false;

    protected override void Awake()
    {
        base.Awake();

        musicPlayer = new AudioPlayer(musicSource);
        sfxPlayer = new AudioPlayer(sfxSource);
    }

    #region Event Subscriptions
    private void OnEnable()
    {
        // UI
        UIEvents.Instance.Hover += () => PlaySound(SoundId.UiHover);
        UIEvents.Instance.Click += () => PlaySound(SoundId.UiClick);

        // Player
        PlayerEvents.Instance.Died += () => PlaySound(SoundId.PlayerDie);

        // Enemy
        EnemyEvents.Instance.Spawned += _ => PlaySound(SoundId.EnemySpawned);
        EnemyEvents.Instance.Died += _ => PlaySound(SoundId.EnemyDie);

        // Game State
        GameStateEvents.Instance.StateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        if (UIEvents.Instance != null)
        {
            UIEvents.Instance.Hover -= () => PlaySound(SoundId.UiHover);
            UIEvents.Instance.Click -= () => PlaySound(SoundId.UiClick);
        }

        if (PlayerEvents.Instance != null)
        {
            PlayerEvents.Instance.Died -= () => PlaySound(SoundId.PlayerDie);
        }

        if (EnemyEvents.Instance != null)
        {
            EnemyEvents.Instance.Spawned -= _ => PlaySound(SoundId.EnemySpawned);
            EnemyEvents.Instance.Died -= _ => PlaySound(SoundId.EnemyDie);
        }

        if (GameStateEvents.Instance != null)
        {
            GameStateEvents.Instance.StateChanged -= OnGameStateChanged;
        }
    }
    #endregion

    // audio per state
    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                PlayMusic(SoundId.MainMenu);
                break;

            case GameState.Gameplay:
                PlayMusic(SoundId.Gameplay);
                break;

            case GameState.Paused:
                break;
        }
}

    public void PlayMusic(SoundId id)
    {
        AudioClip clip = library.Get(id);
        if (clip == null) return;

        musicPlayer.PlayLoop(clip);
        if (debugMode) Debug.Log($"Playing music: {id}");
    }

    public void PlaySound(SoundId id)
    {
        AudioClip clip = library.Get(id);
        if (clip == null) return;

        sfxPlayer.PlayOneShot(clip);
        if (debugMode) Debug.Log($"Played sound: {id}");
    }

    public void Play(AudioClip clip) // for UI button sounds
    {
        sfxPlayer.PlayOneShot(clip);
    }

    public void SetMusicVolume(float value) // for UI sliders
    {
        musicSource.volume = Mathf.Clamp01(value);

        if (debugMode)
            Debug.Log($"Music volume set to {musicSource.volume}");
    }

    public void SetSfxVolume(float value) // for UI sliders
    {
        sfxSource.volume = Mathf.Clamp01(value);

        if (debugMode)
            Debug.Log($"SFX volume set to {sfxSource.volume}");
    }
}