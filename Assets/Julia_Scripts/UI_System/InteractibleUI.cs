using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractibleUI : SingletonBase<InteractibleUI>
{
    public TextMeshProUGUI keyText;
    public InteractableController interactableController;

    private void Update()
    {
        if(interactableController == null) { interactableController = FindFirstObjectByType<InteractableController>(); }
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