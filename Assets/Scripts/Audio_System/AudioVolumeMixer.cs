using UnityEngine;

public class AudioVolumeMixer : MonoBehaviour
{
    private const string MusicKey = "audio_music_volume";
    private const string SfxKey = "audio_sfx_volume";

    public float MusicVolume
    {
        get => PlayerPrefs.GetFloat(MusicKey, 1f);
        set => PlayerPrefs.SetFloat(MusicKey, value);
    }

    public float SfxVolume
    {
        get => PlayerPrefs.GetFloat(SfxKey, 1f);
        set => PlayerPrefs.SetFloat(SfxKey, value);
    }
}
