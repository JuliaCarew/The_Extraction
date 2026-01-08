using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private readonly AudioSource source;

    public AudioPlayer(AudioSource source)
    {
        this.source = source;
        source.spatialBlend = 0f;
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (clip != null)
            source.PlayOneShot(clip);
    }

    public void PlayLoop(AudioClip clip)
    {
        if (clip == null) return;

        source.clip = clip;
        source.loop = true;
        source.Play();
    }

    public void SetVolume(float volume)
    {
        source.volume = volume;
    }
}
