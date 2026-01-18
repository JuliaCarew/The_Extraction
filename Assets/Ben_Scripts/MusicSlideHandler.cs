using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicSlideHandler : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public float musicVolume;
    public float sfxVolume;

    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        if(audioManager == null) 
        {
            try 
            {
                audioManager = FindFirstObjectByType<AudioManager>(); 
            }
            catch 
            {
                Debug.LogError("There no Audio Manager in scene"); 
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSlider.value = 1f;
        sfxSlider.value = 1f;
        musicVolume = musicSlider.value; 
        sfxVolume = sfxSlider.value;    
    }

    // Update is called once per frame
    void Update()
    {
        if(musicSlider.value != musicVolume) 
        {
            musicVolume = musicSlider.value; 
            audioManager.SetMusicVolume(musicVolume);
        }

        if (sfxSlider.value != sfxVolume) 
        {
            sfxVolume = sfxSlider.value;
            audioManager.SetSfxVolume(sfxVolume);
        }
    }
}
