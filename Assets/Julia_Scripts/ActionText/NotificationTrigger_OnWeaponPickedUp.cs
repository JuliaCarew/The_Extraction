using UnityEngine;

public class NotificationTrigger_OnWeaponPickedUp : MonoBehaviour
{
    [Header("Notification Settings")]
    [SerializeField] private NotificationSO weaponPickupNotificationSO;
    [SerializeField] private bool subscribeToEvents = true;

    void OnEnable()
    {
        if (subscribeToEvents)
        {
            // Subscribe to weapon pickup event
            if (PlayerEvents.Instance != null)
            {
                PlayerEvents.Instance.pickedUpWeapon += HandleWeaponPickedUp;
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
                PlayerEvents.Instance.pickedUpWeapon -= HandleWeaponPickedUp;
            }
        }
    }

    void HandleWeaponPickedUp()
    {
        TriggerWeaponPickupNotification();
    }

    public void TriggerWeaponPickupNotification()
    {
        if (weaponPickupNotificationSO == null)
        {
            return;
        }

        if (NotificationManager.Instance != null)
        {
            NotificationManager.Instance.ShowNotification(weaponPickupNotificationSO);
        }
    }
}