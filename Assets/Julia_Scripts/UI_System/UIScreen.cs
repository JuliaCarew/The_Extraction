using UnityEngine;

public abstract class UIScreen : MonoBehaviour, IUIScreen
{
    public bool IsVisible { get; private set; }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        IsVisible = true;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        IsVisible = false;
    }
}