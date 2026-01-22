using System;
using System.Collections;
using UnityEngine;

public class NotificationManager : SingletonBase<NotificationManager>
{
    [Header("References")]
    [SerializeField] private NotificationDisplay notificationDisplay;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private AudioSource audioSource;

    private Coroutine currentAnimationCoroutine;

    protected override void Awake()
    {
        base.Awake();
        
        if (notificationDisplay == null)
        {
            notificationDisplay = FindFirstObjectByType<NotificationDisplay>();
        }
        
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    /// <summary>
    /// Show a notification using a NotificationSO
    /// </summary>
    /// <param name="notificationSO">The ScriptableObject containing notification data</param>
    /// <param name="worldPosition">World position if hasWorldPosition is true</param>
    /// <param name="hasWorldPosition">Whether a world position is provided</param>
    public void ShowNotification(NotificationSO notificationSO, Vector3 worldPosition, bool hasWorldPosition)
    {
        if (notificationSO == null || notificationDisplay == null) 
        {
            Debug.LogWarning("[NotificationManager] Cannot show notification: NotificationSO or NotificationDisplay is null");
            return;
        }
        
        // Stop any existing animation
        if (currentAnimationCoroutine != null)
        {
            StopCoroutine(currentAnimationCoroutine);
        }
        
        // Ensure canvas is active
        if (canvasTransform != null)
        {
            GameObject canvasGameObject = canvasTransform.gameObject;
            if (!canvasGameObject.activeInHierarchy)
            {
                canvasGameObject.SetActive(true);
            }
        }
        
        // Activate the notification display
        if (!notificationDisplay.gameObject.activeSelf)
        {
            notificationDisplay.gameObject.SetActive(true);
        }
        
        // Position calculation
        Vector2 screenPos = CalculatePosition(notificationSO, worldPosition, hasWorldPosition);
        Debug.Log($"[NotificationManager] Calculated screen position: {screenPos}");
        
        // Initialize with the ScriptableObject data
        notificationDisplay.Initialize(notificationSO, screenPos);
        
        // Start animation
        Debug.Log($"[NotificationManager] Starting animation coroutine.");
        currentAnimationCoroutine = StartCoroutine(AnimateNotification(notificationSO));
        
        // Play sound
        if (notificationSO.soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(notificationSO.soundEffect);
        }
    }

    // Show a notification without a world position (uses position from SO)
    public void ShowNotification(NotificationSO notificationSO)
    {
        Vector3 dummyPosition = Vector3.zero;
        ShowNotification(notificationSO, dummyPosition, false);
    }

    Vector2 CalculatePosition(NotificationSO notificationSO, Vector3 worldPosition, bool hasWorldPosition)
    {
        // If world position is provided and the SO allows it
        if (hasWorldPosition && notificationSO.useWorldPosition)
        {
            if (Camera.main == null)
            {
                Debug.LogWarning("[NotificationManager] Camera.main is null, using default position");
                return GetPositionByType(notificationSO.positionType);
            }
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
            return screenPoint;
        }
        
        // Use position type from SO
        return GetPositionByType(notificationSO.positionType, notificationSO.customPosition);
    }

    Vector2 GetPositionByType(NotificationPosition positionType, Vector2 customPos = default)
    {
        float width = Screen.width;
        float height = Screen.height;
        
        switch (positionType)
        {
            case NotificationPosition.ScreenCenter:
                return new Vector2(width / 2f, height / 2f);
            case NotificationPosition.TopCenter:
                return new Vector2(width / 2f, height * 0.9f);
            case NotificationPosition.TopLeft:
                return new Vector2(width * 0.1f, height * 0.9f);
            case NotificationPosition.TopRight:
                return new Vector2(width * 0.9f, height * 0.9f);
            case NotificationPosition.BottomCenter:
                return new Vector2(width / 2f, height * 0.1f);
            case NotificationPosition.BottomLeft:
                return new Vector2(width * 0.1f, height * 0.1f);
            case NotificationPosition.BottomRight:
                return new Vector2(width * 0.9f, height * 0.1f);
            case NotificationPosition.Custom:
                return new Vector2(width * customPos.x, height * customPos.y);
            default:
                return new Vector2(width / 2f, height / 2f);
        }
    }

    IEnumerator AnimateNotification(NotificationSO data)
    {
        // Fade in
        yield return StartCoroutine(notificationDisplay.FadeIn(data.fadeInTime));
        
        // Move and wait
        float elapsed = 0f;
        RectTransform rectTransform = notificationDisplay.GetComponent<RectTransform>();
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = startPos + data.moveOffset;
        
        while (elapsed < data.displayDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / data.displayDuration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
        
        // Fade out 
        yield return StartCoroutine(notificationDisplay.FadeOut(data.fadeOutTime));
        
        // Deactivate after animation
        notificationDisplay.gameObject.SetActive(false);
        currentAnimationCoroutine = null;
        Debug.Log($"[NotificationManager] Animation complete. Notification deactivated.");
    }
}
