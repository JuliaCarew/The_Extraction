using UnityEngine;

public class NotificationTrigger_WhileHiding : MonoBehaviour
{
    [Header("Notification Settings")]
    [SerializeField] private NotificationSO hidingNotificationSO;
    [SerializeField] private bool subscribeToEvents = true;

    void OnEnable()
    {
        if (subscribeToEvents)
        {
            // Subscribe to hiding events
            if (PlayerEvents.Instance != null)
            {
                PlayerEvents.Instance.playerEnterHidingSpot += HandlePlayerEnterHidingSpot;
                PlayerEvents.Instance.playerExitHidingSpot += HandlePlayerExitHidingSpot;
            }
        }
    }

    void OnDisable()
    {
        if (subscribeToEvents)
        {
            // Unsubscribe from events
            if (PlayerEvents.Instance != null)
            {
                PlayerEvents.Instance.playerEnterHidingSpot -= HandlePlayerEnterHidingSpot;
                PlayerEvents.Instance.playerExitHidingSpot -= HandlePlayerExitHidingSpot;
            }
        }
    }

    void HandlePlayerEnterHidingSpot()
    {
        if (hidingNotificationSO != null && NotificationManager.Instance != null)
        {
            NotificationManager.Instance.ShowPersistentNotification(hidingNotificationSO);
        }
    }

    void HandlePlayerExitHidingSpot()
    {
        if (NotificationManager.Instance != null)
        {
            NotificationManager.Instance.HideNotification();
        }
    }
}