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
    private AudioVolumeMixer volumeService;
    public bool debugMode = false;

    protected override void Awake()
    {
        base.Awake();

        musicPlayer = new AudioPlayer(musicSource);
        sfxPlayer = new AudioPlayer(sfxSource);
        volumeService = new AudioVolumeMixer();

        musicPlayer.SetVolume(volumeService.MusicVolume);
        sfxPlayer.SetVolume(volumeService.SfxVolume);
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
        UIEvents.Instance.Hover -= () => PlaySound(SoundId.UiHover);
        UIEvents.Instance.Click -= () => PlaySound(SoundId.UiClick);

        PlayerEvents.Instance.Died -= () => PlaySound(SoundId.PlayerDie);

        EnemyEvents.Instance.Spawned -= _ => PlaySound(SoundId.EnemySpawned);
        EnemyEvents.Instance.Died -= _ => PlaySound(SoundId.EnemyDie);

        GameStateEvents.Instance.StateChanged -= OnGameStateChanged;
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
                //PlaySound(SoundId.WinScreen);
                break;
        }
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

    public void PlayMusic(SoundId id)
    {
        AudioClip clip = library.Get(id);
        if (clip == null) return;

        musicPlayer.PlayLoop(clip);
        if (debugMode) Debug.Log($"Playing music: {id}");
    }
}
