using UnityEngine;

public class NotificationTrigger : MonoBehaviour
{
    [Header("Notification Settings")]
    [SerializeField] private NotificationSO notificationSO;
    [SerializeField] private bool useWorldPosition = false;

    
    public void TriggerNotification(Vector3 worldPosition, bool hasWorldPosition)
    {
        if (notificationSO == null)
        {
            Debug.LogWarning($"[NotificationTrigger] Notification SO is not assigned on {gameObject.name}!");
            return;
        }

        if (NotificationManager.Instance != null)
        {
            Vector3 position = Vector3.zero;
            bool hasPosition = false;
            if (useWorldPosition && hasWorldPosition)
            {
                position = worldPosition;
                hasPosition = true;
            }
            NotificationManager.Instance.ShowNotification(notificationSO, position, hasPosition);
        }
        else
        {
            Debug.LogError("[NotificationTrigger] NotificationManager instance not found!");
        }
    }

    public void TriggerNotification()
    {
        Vector3 dummyPosition = Vector3.zero;
        TriggerNotification(dummyPosition, false);
    }

    public void TriggerNotificationAtThisPosition()
    {
        if (useWorldPosition)
        {
            TriggerNotification(transform.position, true);
        }
        else
        {
            TriggerNotification();
        }
    }

    public void TriggerNotificationAtCenter()
    {
        TriggerNotification();
    }
}