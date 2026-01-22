using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NotificationDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private Image iconComponent;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private RectTransform rectTransform;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
    }
    
    public void Initialize(NotificationSO data, Vector2 screenPosition)
    {
        // Set text 
        if (textComponent != null)
        {
            if (data.showText)
            {
                // Force set the text and color
                if (data.displayText != null)
                {
                    textComponent.text = data.displayText;
                }
                else
                {
                    textComponent.text = "";
                }
                textComponent.color = data.textColor;
                
                // Force UI update
                textComponent.SetAllDirty();
                Canvas.ForceUpdateCanvases();
                
                textComponent.gameObject.SetActive(true);
                Debug.Log($"[NotificationDisplay] Text set: '{textComponent.text}' (from data: '{data.displayText}'), Color: {textComponent.color} (from data: {data.textColor}), ShowText: {data.showText}");
            }
            else
            {
                textComponent.gameObject.SetActive(false);
                Debug.Log($"[NotificationDisplay] Text component disabled (showText: {data.showText})");
            }
        }
        
        // Set icon 
        if (iconComponent != null)
        {
            if (data.showIcon && data.icon != null)
            {
                iconComponent.sprite = data.icon;
                iconComponent.color = Color.white; // Ensure icon is visible
                
                // Force UI update
                iconComponent.SetAllDirty();
                Canvas.ForceUpdateCanvases();
                
                iconComponent.gameObject.SetActive(true);
                Debug.Log($"[NotificationDisplay] Icon set: {iconComponent.sprite.name} (from data: {data.icon.name}), ShowIcon: {data.showIcon}");
            }
            else
            {
                iconComponent.gameObject.SetActive(false);
                Debug.Log($"[NotificationDisplay] Icon component disabled (showIcon: {data.showIcon}, icon: {data.icon != null})");
            }
        }
        
        // Set position
        if (rectTransform != null)
        {
            rectTransform.position = screenPosition;
            Debug.Log($"[NotificationDisplay] Position set to: {screenPosition}, RectTransform position: {rectTransform.position}");
        }
        
        // Reset alpha
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            Debug.Log($"[NotificationDisplay] CanvasGroup alpha reset to 0");
        }
    }
    
    public IEnumerator FadeIn(float duration)
    {
        Debug.Log($"[NotificationDisplay] FadeIn started - duration: {duration}, GameObject: {gameObject.name}");
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (canvasGroup != null)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            }
            yield return null;
        }
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }
    
    public IEnumerator FadeOut(float duration)
    {
        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}