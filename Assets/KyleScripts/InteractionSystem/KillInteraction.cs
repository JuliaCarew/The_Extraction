using UnityEngine;

public class KillInteraction : BaseInteractable
{    
    public override void Interact()
    {
        IKillable victim = GetComponentInParent<IKillable>();
        victim.Kill();
        Debug.Log("KILL!!!"); 
    }
}
