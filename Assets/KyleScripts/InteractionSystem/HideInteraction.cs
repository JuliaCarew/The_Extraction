using UnityEngine;

public class HideInteraction : BaseInteractable{
    
    public override void Interact()
    {
        // Ideally an event would trigger here to notify interaction controller... but for now
        InteractableController interaction = GameObject.Find("Player").GetComponent<InteractableController>();
        interaction.Hide();
        Debug.Log("Interacting with Trash can for example.");
    }

    public override string GetInteractionPrompt()
    {
        return interactionText;
    }
}
