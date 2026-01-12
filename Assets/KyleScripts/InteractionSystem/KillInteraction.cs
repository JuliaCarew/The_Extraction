using UnityEngine;

public class KillInteraction : BaseInteractable
{
    new public string interactionText = "Hide here";
    public override void Interact()
    {
        IKillable victim = GetComponentInParent<IKillable>();
        victim.Kill();
        Debug.Log("KILL!!!"); 
    }
}
