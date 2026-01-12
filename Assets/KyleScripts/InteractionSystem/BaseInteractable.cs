using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteract
{
    [SerializeField] protected string interactionText = "interact";
    public string InteractionText => interactionText;
    public virtual void Interact()
    {
        throw new System.NotImplementedException();
    }

    protected virtual string GetInteractionPrompt()
    {
        return interactionText;
    }
}
   
