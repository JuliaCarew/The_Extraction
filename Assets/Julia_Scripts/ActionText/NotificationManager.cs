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
    private bool isPersistentNotification = false;

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
    public void ShowNotification(NotificationSO notificationSO)
    {
        if (!ValidateNotification(notificationSO)) return;
        
        PrepareNotificationDisplay(notificationSO);
        
        // Animate with auto-hide
        currentAnimationCoroutine = StartCoroutine(AnimateNotification(notificationSO));
        
        PlayNotificationSound(notificationSO);
    }

    /// <summary>
    /// Show a notification that stays visible until HideNotification is called
    /// </summary>
    /// <param name="notificationSO">The ScriptableObject containing notification data</param>
    public void ShowPersistentNotification(NotificationSO notificationSO)
    {
        if (!ValidateNotification(notificationSO)) return;
        
        PrepareNotificationDisplay(notificationSO);
        
        // Fade in only 
        StartCoroutine(notificationDisplay.FadeIn(notificationSO.fadeInTime));
        
        // Mark as persistent
        isPersistentNotification = true;
        
        PlayNotificationSound(notificationSO);
    }

    public void HideNotification(float fadeOutTime = 0.3f)
    {
        if (notificationDisplay == null)
        {
            return;
        }

        // Stop any animation
        if (currentAnimationCoroutine != null)
        {
            StopCoroutine(currentAnimationCoroutine);
            currentAnimationCoroutine = null;
        }

        // Fade out and deactivate
        if (notificationDisplay.gameObject.activeSelf)
        {
            currentAnimationCoroutine = StartCoroutine(HideNotificationCoroutine(fadeOutTime));
        }

        isPersistentNotification = false;
    }

    private bool ValidateNotification(NotificationSO notificationSO)
    {
        if (notificationSO == null || notificationDisplay == null)
        {
            Debug.LogWarning("[NotificationManager] Cannot show notification: NotificationSO or NotificationDisplay is null");
            return false;
        }
        return true;
    }

    private void PrepareNotificationDisplay(NotificationSO notificationSO)
    {
        // Stop any existing animation
        if (currentAnimationCoroutine != null)
        {
            StopCoroutine(currentAnimationCoroutine);
        }
        
        // Ensure canvas is active
        ActivateCanvas();
        
        // Activate the notification display
        if (!notificationDisplay.gameObject.activeSelf)
        {
            notificationDisplay.gameObject.SetActive(true);
        }
        
        // Set position from SO
        Vector2 screenPos = CalculatePosition(notificationSO);
        
        // Initialize 
        notificationDisplay.Initialize(notificationSO, screenPos);
    }

    private void ActivateCanvas()
    {
        if (canvasTransform != null)
        {
            GameObject canvasGameObject = canvasTransform.gameObject;
            if (!canvasGameObject.activeInHierarchy)
            {
                canvasGameObject.SetActive(true);
            }
        }
    }

    private void PlayNotificationSound(NotificationSO notificationSO)
    {
        if (notificationSO.soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(notificationSO.soundEffect);
        }
    }

    IEnumerator HideNotificationCoroutine(float fadeOutTime)
    {
        yield return StartCoroutine(notificationDisplay.FadeOut(fadeOutTime));
        notificationDisplay.gameObject.SetActive(false);
        currentAnimationCoroutine = null;
    }

    Vector2 CalculatePosition(NotificationSO notificationSO)
    {
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