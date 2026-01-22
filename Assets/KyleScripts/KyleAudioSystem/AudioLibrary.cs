using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class AudioLibrary : MonoBehaviour
{
    List<AudioClip> musicClips = new List<AudioClip>();
    List<AudioClip> sfxClips = new List<AudioClip>();

    Dictionary<string, AudioClip> musicDict = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        LoadClips();
        PopulateAudioDictionaries();
    }

    private void LoadClips()
    {
        // Get all clips from folder.
        musicClips.AddRange(Resources.LoadAll<AudioClip>("Audio/Music"));
        sfxClips.AddRange(Resources.LoadAll<AudioClip>("Audio/SFX"));
    }

    private void PopulateAudioDictionaries()
    {
        // Add music to dictionary.
        foreach (var clip in musicClips)
        {
            if (!musicDict.ContainsKey(clip.name))
            {
                musicDict.Add(clip.name, clip);
                Debug.Log($"Added music clip: {clip.name}");
            }
        }
        // Add sound effects to dictionary.
        foreach (var clip in sfxClips)
        {
            if (!sfxDict.ContainsKey(clip.name))
            {
                sfxDict.Add(clip.name, clip);
            }
        }
    }

    public AudioClip GetMusicClip(string id)
    {
        musicDict.TryGetValue(id, out AudioClip clip);
        return clip;
    }

    public AudioClip GetSFXClip(string id)
    {
        sfxDict.TryGetValue(id, out AudioClip clip);
        return clip;
    }
}
