using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableController : SingletonBase<InteractableController>
{
    [SerializeField] private float detectionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private LayerMask hiddenLayer;
    [SerializeField] private PlayerMovement playerController;
    [SerializeField] private PickUpController pickUpController;

    public BaseInteractable currentInteractable;
    private InputManager input;
    private MeshRenderer[] meshRenderers;
    private bool isHidden = false;
    [SerializeField] Vector3 offset;
    private BaseInteractable previousInteractable;
    private string originalPrompt = "";
    [SerializeField] private PlayerSightRange playerSightRange;

    public bool hasWeapon => pickUpController != null && pickUpController.hasWeapon;

    private void Awake()
    {
        input = GetComponentInChildren<InputManager>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        
        if (pickUpController == null)
        {
            pickUpController = GetComponent<PickUpController>();
            if (pickUpController == null)
            {
                pickUpController = GetComponentInParent<PickUpController>();
            }
        }
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
            meshRenderer.gameObject.layer = LayerMask.NameToLayer("Hidden");
            meshRenderer.enabled = false;
        }
    }

    private void EnableMeshes()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
            meshRenderer.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void DetectInteractable()
    {
        if (Physics.Raycast(transform.position + offset, transform.forward, out RaycastHit hit, detectionDistance, interactionLayer))
        {
            currentInteractable = hit.collider.GetComponent<BaseInteractable>();
                
            // store original prompt
            if (currentInteractable != null && currentInteractable != previousInteractable)
            {
                originalPrompt = currentInteractable.GetInteractionPrompt();
                previousInteractable = currentInteractable;
            }
                
            // update interaction prompt based on weapon status
            UpdateInteractionPrompt();
        }
        else
        {
            currentInteractable = null;
            previousInteractable = null;
        }
    }

    private bool IsEnemyInteractable()
    {
        if (currentInteractable == null)
            return false;
            
        return currentInteractable.gameObject.CompareTag("Enemy");
    }

    private void UpdateInteractionPrompt()
    {
        if (currentInteractable == null || string.IsNullOrEmpty(originalPrompt))
            return;

        if (IsEnemyInteractable())
        {
            if (!hasWeapon)
            {
                currentInteractable.SetInteractionPrompt("Get a weapon!");
            }
            else // default prompt when player has a weapon
            {
                currentInteractable.SetInteractionPrompt(originalPrompt);
            }
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
            if (currentInteractable != null)
            {
                // need weapon for enemy interactables
                if (IsEnemyInteractable() && !hasWeapon)
                {
                    return;
                }
                
                currentInteractable.Interact();
            }
        }
    }

    public void Hide()
    {
        gameObject.layer = LayerMask.NameToLayer("Hidden");
        playerController.DisableMovement();
        isHidden = true;
        DisableMeshes();
        playerSightRange.DisablePlayerSightRadius();
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
        Gizmos.DrawRay(transform.position + offset, transform.forward * detectionDistance);
    }
}
