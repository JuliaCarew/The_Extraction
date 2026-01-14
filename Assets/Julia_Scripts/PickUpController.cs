using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public bool hasWeapon { get; private set; } = false;
    [SerializeField] private Transform weaponHolder;

    public bool debugMode = false;

    private void OnEnable()
    {
        if(debugMode) Debug.Log("PickUpController: OnEnable - Subscribing to pickedUpWeapon event");
        PlayerEvents.Instance.pickedUpWeapon += PickedUpWeapon;
    }

    private void OnDestroy()
    {
        PlayerEvents.Instance.pickedUpWeapon -= PickedUpWeapon;
    }

    private void PickedUpWeapon()
    {
        if(debugMode)Debug.Log("PickUpController: PickedUpWeapon event handler called!");
        if(debugMode) Debug.Log($"PickUpController: LastPickedUpWeapon = {(Pickup.LastPickedUpWeapon != null ? Pickup.LastPickedUpWeapon.name : "null")}, weaponHolder = {(weaponHolder != null ? weaponHolder.name : "null")}");
        
        if (Pickup.LastPickedUpWeapon != null && weaponHolder != null)
        {
            GameObject weaponObj = Pickup.LastPickedUpWeapon;
            
            // Move weapon to holder and parent it
            weaponObj.transform.position = weaponHolder.position;
            weaponObj.transform.rotation = weaponHolder.rotation;
            weaponObj.transform.SetParent(weaponHolder);
            
            // Disable the pickup component 
            Pickup pickupComponent = weaponObj.GetComponent<Pickup>();
            if (pickupComponent != null)
            {
                pickupComponent.enabled = false;
            }
            
            Pickup.LastPickedUpWeapon = null;
            
            hasWeapon = true;
            if(debugMode)Debug.Log("PickUpController: player picked up weapon successfully!");
        }
        else
        {
            if(debugMode)Debug.LogWarning($"PickUpController: No weapon pickup found or weaponHolder is null. LastPickedUpWeapon: {(Pickup.LastPickedUpWeapon != null ? "exists" : "null")}, weaponHolder: {(weaponHolder != null ? "exists" : "null")}");
        }
    }
}