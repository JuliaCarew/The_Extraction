using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteract
{
    [SerializeField] protected string interactionText = "interact";
    public virtual void Interact()
    {
        throw new System.NotImplementedException();
    }

    public virtual string GetInteractionPrompt()
    {
        return interactionText;
    }
}
   
