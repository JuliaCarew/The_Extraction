using UnityEngine;

[CreateAssetMenu(fileName = "NotificationSO", menuName = "Scriptable Objects/NotificationSO")]
public class NotificationSO : ScriptableObject
{
    [Header("Display Settings")]
    public string displayText;
    public Sprite icon;
    public Color textColor = Color.white;

    [Header("Position Settings")]
    public NotificationPosition positionType = NotificationPosition.ScreenCenter;
    public Vector2 customPosition = new Vector2(0.5f, 0.5f); // Normalized screen position (0-1)
    public bool useWorldPosition = false; // If true, position will be provided at runtime

    [Header("Animation Settings")]
    public float displayDuration = 2.0f;
    public float fadeInTime = 0.2f;
    public float fadeOutTime = 0.5f;
    public Vector2 moveOffset = new Vector2(0, 50);

    [Header("Sound")]
    public AudioClip soundEffect; // Optional sound effect

    [Header("Display Type")]
    public bool showIcon = true;
    public bool showText = true;
}

public enum NotificationPosition
{
    ScreenCenter,
    TopCenter,
    TopLeft,
    TopRight,
    BottomCenter,
    BottomLeft,
    BottomRight,
    Custom
}
