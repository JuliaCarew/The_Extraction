using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractibleUI : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    private InteractableController interactableController;

    private void UpdateInteractText()
    {
        var interactible = interactableController.currentInteractable;
        if(interactible != null)
        {
            keyText.text = interactible.GetInteractionPrompt();
            keyText.gameObject.SetActive(true);
        }
        else
        {
            keyText.gameObject.SetActive(false);
        }            
    }
}
