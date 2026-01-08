using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]
public class AudioData : ScriptableObject
{
    [Serializable]
    public struct Entry
    {
        public SoundId id;
        public AudioClip clip;
    }

    [SerializeField] private List<Entry> entries;

    private Dictionary<SoundId, AudioClip> lookup;
    
    // load the sound from dictionary for fast lookup
    public AudioClip Get(SoundId id)
    {
        lookup ??= entries.ToDictionary(e => e.id, e => e.clip);
        return lookup.TryGetValue(id, out var clip) ? clip : null;
    }
}
