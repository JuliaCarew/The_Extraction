using UnityEngine;

public class NotificationTrigger_OnKill : MonoBehaviour
{
    [Header("Notification Settings")]
    [SerializeField] private NotificationSO killNotificationSO;
    [SerializeField] private bool useEnemyPosition = true;
    [SerializeField] private bool subscribeToEvents = true;

    private Vector3 lastKilledEnemyPosition;
    private bool hasLastKilledEnemyPosition = false;

    void OnEnable()
    {
        if (subscribeToEvents)
        {
            // Subscribe to enemy kill event
            if (EnemyEvents.Instance != null)
            {
                EnemyEvents.Instance.OnEnemyKilled += HandleEnemyKilled;
                EnemyEvents.Instance.Died += HandleEnemyDied;
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
                EnemyEvents.Instance.Died -= HandleEnemyDied;
            }
        }
    }

    void HandleEnemyKilled()
    {
        // Use the last killed enemy position if available
        if (useEnemyPosition && hasLastKilledEnemyPosition)
        {
            TriggerKillNotification(lastKilledEnemyPosition, true);
            hasLastKilledEnemyPosition = false; // Reset after use
        }
        else
        {
            TriggerKillNotification();
        }
    }

    void HandleEnemyDied(GameObject enemy)
    {
        // Store the enemy position for when OnEnemyKilled is called
        if (enemy != null)
        {
            lastKilledEnemyPosition = enemy.transform.position;
            hasLastKilledEnemyPosition = true;
        }
    }

    public void TriggerKillNotification(Vector3 enemyPosition, bool hasEnemyPosition)
    {
        if (killNotificationSO == null)
        {
            Debug.LogWarning("[NotificationTrigger_OnKill] Kill notification SO is not assigned!");
            return;
        }

        if (NotificationManager.Instance != null)
        {
            Vector3 position = Vector3.zero;
            bool hasPosition = false;
            if (useEnemyPosition && hasEnemyPosition)
            {
                position = enemyPosition;
                hasPosition = true;
            }
            NotificationManager.Instance.ShowNotification(killNotificationSO, position, hasPosition);
        }
        else
        {
            Debug.LogError("[NotificationTrigger_OnKill] NotificationManager instance not found!");
        }
    }

    public void TriggerKillNotification()
    {
        Vector3 dummyPosition = Vector3.zero;
        TriggerKillNotification(dummyPosition, false);
    }
}