using System;
using System.Collections;
using UnityEngine;

public class ActionTextManager : SingletonBase<ActionTextManager>
{
    [Header("References")]
    [SerializeField] private ActionTextDisplay actionTextDisplay;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private AudioSource audioSource;

    private Coroutine currentAnimationCoroutine;

    protected override void Awake()
    {
        base.Awake();
        
        if (actionTextDisplay == null)
        {
            actionTextDisplay = FindFirstObjectByType<ActionTextDisplay>();
        }
    }

    public void ShowActionText(ActionTextSO actionTextSO, Vector3? worldPosition = null)
    {
        if (actionTextSO == null || actionTextDisplay == null) return;
        
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
        
        // Activate the action text display
        if (!actionTextDisplay.gameObject.activeSelf)
        {
            actionTextDisplay.gameObject.SetActive(true);
        }
        
        // Position calculation
        Vector2 screenPos = CalculatePosition(worldPosition);
        Debug.Log($"[ActionTextManager] Calculated screen position: {screenPos}");
        
        // Initialize with the ScriptableObject data
        actionTextDisplay.Initialize(actionTextSO, screenPos);
        
        // Start animation (would need to check if its a hiding spot)
        Debug.Log($"[ActionTextManager] Starting animation coroutine.");
        currentAnimationCoroutine = StartCoroutine(AnimateText(actionTextSO));
        
        // Play sound
        if (actionTextSO.soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(actionTextSO.soundEffect);
        }
    }

    Vector2 CalculatePosition(Vector3? worldPosition)
    {
        if (worldPosition.HasValue)
        {
            // Convert world position to screen space
            if (Camera.main == null)
            {
                return Vector2.zero;
            }
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPosition.Value);
            return screenPoint;
        }
        else
        {
            // Default center position
            Vector2 centerPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
            return centerPos;
        }
    }

    IEnumerator AnimateText(ActionTextSO data)
    {
        // Fade in
        yield return StartCoroutine(actionTextDisplay.FadeIn(data.fadeInTime));
        
        // Move and wait
        float elapsed = 0f;
        RectTransform rectTransform = actionTextDisplay.GetComponent<RectTransform>();
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
        yield return StartCoroutine(actionTextDisplay.FadeOut(data.fadeOutTime));
        
        // Deactivate after animation
        actionTextDisplay.gameObject.SetActive(false);
        currentAnimationCoroutine = null;
        Debug.Log($"[ActionTextManager] Animation complete. Action text deactivated.");
    }
}
