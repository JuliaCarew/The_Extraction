using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private Pickup pickup;
    private bool isHoldingWeapon = false;
    [SerializeField] private Transform weaponHolder;

    private void OnEnable()
    {
        PlayerEvents.Instance.pickedUpWeapon += PickedUpWeapon;
    }

    private void OnDestroy()
    {
        PlayerEvents.Instance.pickedUpWeapon -= PickedUpWeapon;
    }

    private void PickedUpWeapon()
    {
        // set weapon to holder's position
        var pickupObj = pickup.gameObject;
        pickupObj.transform.position = weaponHolder.transform.position;

        isHoldingWeapon = true;
        Debug.Log("PickUpController: player picked up weapon");
    }
}