using UnityEngine;

public class NotificationTrigger : MonoBehaviour
{
    [Header("Notification Settings")]
    [SerializeField] private NotificationSO notificationSO;

    public void TriggerNotification()
    {
        if (notificationSO == null)
        {
            Debug.LogWarning($"[NotificationTrigger] Notification SO is not assigned on {gameObject.name}!");
            return;
        }

        if (NotificationManager.Instance != null)
        {
            NotificationManager.Instance.ShowNotification(notificationSO);
        }
        else
        {
            Debug.LogError("[NotificationTrigger] NotificationManager instance not found!");
        }
    }
}