using UnityEngine;

public class UIButtonAudio : MonoBehaviour, IUIButtonAction
{
    [SerializeField] private SoundId clickSound;
    [SerializeField] private SoundId hoverSound;

    public void OnButtonPressed()
    {
        AudioManager.Instance.PlaySound(clickSound);
    }
    public void OnButtonHovered()
    {
        AudioManager.Instance.PlaySound(hoverSound);
    }
}