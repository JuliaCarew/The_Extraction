using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableController : SingletonBase<InteractableController>
{
    [SerializeField] private float detectionRadius = 1.5f;
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
    private Collider[] interactionResults = new Collider[5];

    public bool hasWeapon => pickUpController != null && pickUpController.hasWeapon;

    protected override void Awake()
    {
        base.Awake();
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
        Vector3 center = transform.position + (transform.forward * offset.z) + (transform.up * offset.y);
        int hitCount = Physics.OverlapSphereNonAlloc(center, detectionRadius, interactionResults, interactionLayer);
        if (hitCount > 0)
        {
            currentInteractable = interactionResults[0].GetComponent<BaseInteractable>();

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
        Vector3 detectionCenter = transform.position + (transform.forward * offset.z) + (transform.up * offset.y);

        if (currentInteractable != null)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(detectionCenter, detectionRadius);
    }
}
