using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [SerializeField] private float detectionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;

    private IInteract currentInteractable;
    private InputManager input;

    private void Update()
    {
        DetectInteractable();
        input = TryGetComponent<InputManager>(out input) ? input : null;
    }

    private void DetectInteractable()
    {
        if (Physics.Raycast(transform.position, transform.forward, detectionDistance, interactionLayer))
        {
            currentInteractable = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectionDistance, interactionLayer) 
                ? hit.collider.GetComponent<IInteract>() 
                : null;
        }
    }

    private void OnInteract()
    {
        currentInteractable?.Interact();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);
    }
}
