using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteract
{
    [SerializeField] protected string interactionPrompt;
    public virtual void Interact()
    {
        throw new System.NotImplementedException();
    }

    protected virtual string GetInteractionPrompt()
    {
        return interactionPrompt;
    }
}
   
