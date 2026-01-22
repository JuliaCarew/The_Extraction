using UnityEngine;

public class NotificationTrigger_OnWeaponPickedUp : MonoBehaviour
{
    [Header("Notification Settings")]
    [SerializeField] private NotificationSO weaponPickupNotificationSO;
    [SerializeField] private bool usePickupPosition = false;
    [SerializeField] private bool subscribeToEvents = true;

    private Vector3 lastWeaponPickupPosition;
    private bool hasLastWeaponPickupPosition = false;

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
        // Use the last weapon pickup position if available
        if (usePickupPosition && hasLastWeaponPickupPosition)
        {
            TriggerWeaponPickupNotification(lastWeaponPickupPosition, true);
            hasLastWeaponPickupPosition = false; // Reset after use
        }
        else
        {
            TriggerWeaponPickupNotification();
        }
    }

    public void TriggerWeaponPickupNotification(Vector3 weaponPosition, bool hasWeaponPosition)
    {
        if (weaponPickupNotificationSO == null)
        {
            Debug.LogWarning("[NotificationTrigger_OnWeaponPickedUp] Weapon pickup notification SO is not assigned!");
            return;
        }

        if (NotificationManager.Instance != null)
        {
            Vector3 position = Vector3.zero;
            bool hasPosition = false;
            if (usePickupPosition && hasWeaponPosition)
            {
                position = weaponPosition;
                hasPosition = true;
            }
            NotificationManager.Instance.ShowNotification(weaponPickupNotificationSO, position, hasPosition);
        }
        else
        {
            Debug.LogError("[NotificationTrigger_OnWeaponPickedUp] NotificationManager instance not found!");
        }
    }

    public void TriggerWeaponPickupNotification()
    {
        Vector3 dummyPosition = Vector3.zero;
        TriggerWeaponPickupNotification(dummyPosition, false);
    }

    // Method to set weapon pickup position (call this before the event fires if you need position)
    public void SetWeaponPickupPosition(Vector3 position)
    {
        lastWeaponPickupPosition = position;
        hasLastWeaponPickupPosition = true;
    }

}