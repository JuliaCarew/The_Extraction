using UnityEngine;

public class KillInteraction : BaseInteractable
{
    new public string interactionText = "Hide here";
    public override void Interact()
    {
        Debug.Log("KILL!!!"); 
    }
}
