using UnityEngine;

// only responsible for showing action text
public class ActionTextController : MonoBehaviour
{
    [Header("Action Text Definitions")]
    [SerializeField] private ActionTextSO killActionData;
    [SerializeField] private ActionTextSO hideActionData;

    BaseInteractable currentInteract = InteractableController.Instance.currentInteractable;

    private void Update()
    {
        if (currentInteract == null)
            return;
        else
            DetectInteractForActionText();
    }

    void DetectInteractForActionText()
    {
        if (currentInteract.gameObject.CompareTag("Enemy"))
        {
            KillActionText();
            Debug.Log("[InteractableController] current interactible tagged Enemy");
        }
        else if (currentInteract.gameObject.CompareTag("Hide"))
        {
            HideActionText();
            Debug.Log("[InteractableController] current interactible tagged Hide");
        }
    }

    void KillActionText()
    {
        // Store enemy position before interaction 
        bool wasEnemy = InteractableController.Instance.IsEnemyInteractable();
        Vector3? enemyPosition = null;
        if (wasEnemy && killActionData != null)
        {
            enemyPosition = currentInteract.transform.position;
        }

        // Show kill action text if enemy was killed
        if (wasEnemy)
        {
            if (killActionData != null && enemyPosition.HasValue && ActionTextManager.Instance != null)
            {
                Debug.Log($"[InteractableController] Calling ShowActionText for kill");
                ActionTextManager.Instance.ShowActionText(killActionData, null);
            }
        }
    }

    void HideActionText()
    {
        if (hideActionData != null && ActionTextManager.Instance != null)
        {
            Debug.Log($"[InteractableController] Calling ShowActionText for hide (screen center)");
            ActionTextManager.Instance.ShowActionText(hideActionData, null);
            // need to keep showing when hiding 
        }
    }
}