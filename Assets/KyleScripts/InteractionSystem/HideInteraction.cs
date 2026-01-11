using UnityEngine;

public class HideInteraction : BaseInteractable
{
    public override void Interact()
    {
        PlayerController playerCont = FindFirstObjectByType<PlayerController>();
        playerCont.HideInObject(); 
        Debug.Log("Interacting with Trash can for example.");
    }
}
