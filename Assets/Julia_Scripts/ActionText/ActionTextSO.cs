using UnityEngine;

[CreateAssetMenu(fileName = "ActionTextSO", menuName = "Scriptable Objects/ActionTextSO")]
public class ActionTextSO : ScriptableObject
{
    [Header("Display Settings")]
    public string displayText;
    public Sprite icon;
    public Color textColor = Color.white;

    [Header("Animation Settings")]
    public float displayDuration  = 2.0f;
    public float fadeInTime = 0.2f;
    public float fadeOutTime = 0.5f;
    public Vector2 moveOffset = new Vector2(0, 50);

    [Header("Sound")]
    public AudioClip soundEffect;

    [Header("Display Type")]
    public bool showIcon = true;
    public bool showText = true;
}
