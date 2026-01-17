using UnityEngine;

public class BaseTooth : MonoBehaviour
{
    [Header("Tooth Properties")]
    [SerializeField] protected ToothType toothType;
    [SerializeField] protected float pickUpRadius = 1.5f;
    [SerializeField] protected LayerMask playerLayer;
    
    protected bool isPickedUp = false;

    protected virtual void Start()
    {
        if (playerLayer == 0)
        {
            playerLayer = LayerMask.GetMask("Player");
        }
    }

    protected virtual void Update()
    {
        if (!isPickedUp)
        {
            CheckForPickup();
        }
    }

    protected virtual void CheckForPickup()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpRadius, playerLayer);
        
        if (colliders.Length > 0)
        {
            ToothInventory inventory = colliders[0].GetComponent<ToothInventory>();
            if (inventory != null && inventory.TryAddTooth(GetToothType()))
            {
                OnPickedUp();
            }
        }
    }

    protected virtual void OnPickedUp()
    {
        isPickedUp = true;
        // apply actiontext here
        Destroy(gameObject);
    }

    public virtual ToothType GetToothType()
    {
        return toothType;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
