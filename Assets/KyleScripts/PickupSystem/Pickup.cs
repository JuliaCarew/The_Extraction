using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] public PickupType type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (type == PickupType.Teeth)
            {
                PlayerEvents.Instance.ToothCollected();
            }
            if (type == PickupType.Money)
            {
                PlayerEvents.Instance.MoneyCollected();
            }
            if (type == PickupType.Weapon)
            {
                PlayerEvents.Instance.WeaponPickedUp();
            }
            Destroy(gameObject);
        }
    }
}

public enum PickupType
{
    Teeth,
    Money,
    Weapon
}
