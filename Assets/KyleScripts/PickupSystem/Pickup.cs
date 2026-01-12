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
                PlayerEvents.Instance.ToothCollected();
            }
            if (type == PickupType.Money)
            {
                PlayerEvents.Instance.MoneyCollected();
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
