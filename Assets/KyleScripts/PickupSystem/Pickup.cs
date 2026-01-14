using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] public PickupType type;
    public static GameObject LastPickedUpWeapon { get; set; }
    public bool debugMode = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (debugMode) Debug.Log($"Pickup: OnTriggerEnter called with {other.gameObject.name}, tag: {other.gameObject.tag}, type: {type}");
        
        // check if the object or its parent has the "Player" tag
        GameObject playerObject = other.gameObject;
        
        if (!playerObject.CompareTag("Player"))
        {
            // check parent objects
            if (playerObject.transform.parent != null)
            {
                playerObject = playerObject.transform.parent.gameObject;
                if (debugMode) Debug.Log($"Pickup: Checking parent object: {playerObject.name}, tag: {playerObject.tag}");
            }
        }
        
        // check root object if still not tagged
        if (!playerObject.CompareTag("Player"))
        {
            playerObject = other.gameObject.transform.root.gameObject;
            if (debugMode) Debug.Log($"Pickup: Checking root object: {playerObject.name}, tag: {playerObject.tag}");
        }
        
        if (playerObject.CompareTag("Player"))
        {
            if (debugMode) Debug.Log($"Pickup: Player collision detected! Type: {type}");
            
            if (type == PickupType.Teeth)
            {
                PlayerEvents.Instance.ToothCollected();
                Destroy(gameObject);
            }
            else if (type == PickupType.Money)
            {
                PlayerEvents.Instance.MoneyCollected();
                Destroy(gameObject);
            }
            else if (type == PickupType.Weapon)
            {
                LastPickedUpWeapon = gameObject;
                PlayerEvents.Instance.WeaponPickedUp();
                if (debugMode) Debug.Log($"Pickup: WeaponPickedUp() called, LastPickedUpWeapon = {(LastPickedUpWeapon != null ? LastPickedUpWeapon.name : "null")}");
            }
        }
        else
        {
            if (debugMode) Debug.Log($"Pickup: Collision with non-Player object: {other.gameObject.name}");
        }
    }
}

public enum PickupType
{
    Teeth,
    Money,
    Weapon
}