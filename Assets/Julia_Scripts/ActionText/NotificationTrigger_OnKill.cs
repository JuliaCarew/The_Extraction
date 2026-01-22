using UnityEngine;

public class NotificationTrigger_OnKill : MonoBehaviour
{
    [Header("Notification Settings")]
    [SerializeField] private NotificationSO killNotificationSO;
    [SerializeField] private bool subscribeToEvents = true;

    void OnEnable()
    {
        if (subscribeToEvents)
        {
            // Subscribe to enemy kill event
            if (EnemyEvents.Instance != null)
            {
                EnemyEvents.Instance.OnEnemyKilled += HandleEnemyKilled;
            }
        }
    }

    void OnDisable()
    {
        if (subscribeToEvents)
        {
            // Unsubscribe from events
            if (EnemyEvents.Instance != null)
            {
                EnemyEvents.Instance.OnEnemyKilled -= HandleEnemyKilled;
            }
        }
    }

    void HandleEnemyKilled()
    {
        TriggerKillNotification();
    }

    public void TriggerKillNotification()
    {
        if (killNotificationSO == null)
        {
            return;
        }

        if (NotificationManager.Instance != null)
        {
            NotificationManager.Instance.ShowNotification(killNotificationSO);
        }
    }
}