using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableController : MonoBehaviour
{
    [SerializeField] private float detectionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private LayerMask hiddenLayer;
    [SerializeField] private PlayerMovement playerController;

    public BaseInteractable currentInteractable;
    private InputManager input;
    private MeshRenderer[] meshRenderers;
    private bool isHidden = false;

    private void Awake()
    {
        input = GetComponentInChildren<InputManager>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
    }

    private void OnEnable()
    {
        input.InteractInputEvent += OnInteract;
    }

    private void OnDestroy()
    {
        input.InteractInputEvent -= OnInteract;
    }
    private void Update()
    {
        DetectInteractable();
    }

    private void DisableMeshes()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }

    private void EnableMeshes()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }
    }

    private void DetectInteractable()
    {
        if (Physics.Raycast(transform.position, transform.forward, detectionDistance, interactionLayer))
        {
            currentInteractable = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectionDistance, interactionLayer) 
                ? hit.collider.GetComponent<BaseInteractable>() 
                : null;
        }
        else
        {
            currentInteractable = null;
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (isHidden) 
        {
            StopHiding();
        }
        else
        {
            currentInteractable?.Interact();
        }
    }

    public void Hide()
    {
        gameObject.layer = LayerMask.NameToLayer("Hidden");
        playerController.DisableMovement();
        isHidden = true;
        DisableMeshes();
    }

    private void StopHiding()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
        playerController.EnableMovement();
        isHidden = false;
        EnableMeshes();
    }

    private void OnDrawGizmos()
    {
        if (currentInteractable != null)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(transform.position, transform.forward * detectionDistance);
    }
}
