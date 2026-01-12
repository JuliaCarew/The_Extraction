using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (type == PickupType.Teeth)
            {
                PlayerEvents.ToothCollected();
            }
            if (type == PickupType.Money)
            {
                PlayerEvents.MoneyCollected();
            }
            Destroy(gameObject);
        }
    }
}

public enum PickupType
{
    Teeth,
    Money
}
