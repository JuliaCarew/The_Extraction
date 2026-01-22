using UnityEngine;

// can be removed
public class ActionTextController : MonoBehaviour
{
    [Header("Notification Definitions (Legacy)")]
    [SerializeField] private NotificationSO killNotificationSO;
    [SerializeField] private NotificationSO hideNotificationSO;

    [Header("Optional References")]
    [SerializeField] private MonoBehaviour interactableControllerRef; // Generic reference if needed

    public void ShowKillNotification(Vector3 enemyPosition, bool hasEnemyPosition)
    {
        if (killNotificationSO != null && NotificationManager.Instance != null)
        {
            NotificationManager.Instance.ShowNotification(killNotificationSO, enemyPosition, hasEnemyPosition);
        }
    }

    public void ShowKillNotification()
    {
        Vector3 dummyPosition = Vector3.zero;
        ShowKillNotification(dummyPosition, false);
    }

    public void ShowHideNotification()
    {
        if (hideNotificationSO != null && NotificationManager.Instance != null)
        {
            NotificationManager.Instance.ShowNotification(hideNotificationSO);
        }
    }

    public void OnEnemyKilled(Vector3 enemyPosition)
    {
        ShowKillNotification(enemyPosition, true);
    }

    public void OnHideSpotEntered()
    {
        ShowHideNotification();
    }
}