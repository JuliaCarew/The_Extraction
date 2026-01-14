using UnityEngine;

public class KillInteraction : BaseInteractable
{
    public override void Interact()
    {
        IKillable victim = GetComponentInParent<IKillable>();
        victim.Kill();
        // Note to any programmers, the IKillable is on enemy's execution handler, which destroys the enemy game object,
        // thereforce any code after victim.Kill() will not run and events should be handled in the execution handler.
        Debug.Log("KILL!!!");
    }

    public override string GetInteractionPrompt()
    {
        return interactionText;
    }
}
