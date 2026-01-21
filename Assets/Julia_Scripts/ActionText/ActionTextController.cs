using UnityEngine;

public class ActionTextController : MonoBehaviour
{
    [Header("Action Text Definitions")]
    [SerializeField] private ActionTextSO killActionData;
    [SerializeField] private ActionTextSO hideActionData;
    
    void KillActionText()
    {
        // kill action

        // Store enemy position before interaction 
        //bool wasEnemy = IsEnemyInteractable();
        //Vector3? enemyPosition = null;
        //if (wasEnemy && killActionData != null)
        //{
        //    enemyPosition = currentInteractable.transform.position;
        //}

        //// Show kill action text if enemy was killed
        //if (wasEnemy)
        //{
        //    if (killActionData != null && enemyPosition.HasValue && ActionTextManager.Instance != null)
        //    {
        //        Debug.Log($"[InteractableController] Calling ShowActionText for kill");
        //        ActionTextManager.Instance.ShowActionText(killActionData, null);
        //    }
        //}
    }

    void HideActionText()
    {
        // hide action

        // Show hide action text 
        if (hideActionData != null && ActionTextManager.Instance != null)
        {
            Debug.Log($"[InteractableController] Calling ShowActionText for hide (screen center)");
            ActionTextManager.Instance.ShowActionText(hideActionData, null);
            // need to keep showing when hiding 
        }
    }
}
