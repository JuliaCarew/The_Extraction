using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractableController : SingletonBase<InteractableController>
{
    [SerializeField] private float detectionRadius = 1.5f;
    #region Variables 

    [Header("Action Text Definitions")]
    [SerializeField] private ActionTextSO killActionData;
    [SerializeField] private ActionTextSO hideActionData;

    [Header("Interaction Detection")]
    [SerializeField] private float detectionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private LayerMask hiddenLayer;

    [Header("References")]
    [SerializeField] private PlayerMovement playerController;
    [SerializeField] private PickUpController pickUpController;
    [SerializeField] private GameObject interactibleTextGameObject;
    [SerializeField] private string interactibleTextGameObjectName = "InteractibleUI"; // Name to search for if reference is lost
    private InputManager input;

    [Header("Interactible Settings")]
    public BaseInteractable currentInteractable;
    private BaseInteractable previousInteractable;
    private MeshRenderer[] meshRenderers;
    private bool isHidden = false;
    [SerializeField] Vector3 offset;
    private string originalPrompt = "";
    [SerializeField] private PlayerSightRange playerSightRange;
    private Collider[] interactionResults = new Collider[5];
    private bool previousWeaponState = false;

    public bool hasWeapon => pickUpController != null && pickUpController.hasWeapon;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        //input = GetComponentInChildren<InputManager>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>(true);

        if (pickUpController == null)
        {
            pickUpController = GetComponent<PickUpController>();
            if (pickUpController == null)
            {
                pickUpController = GetComponentInParent<PickUpController>();
            }
        }

        // Try to find interactible UI if reference is lost
        FindInteractibleUI();
    }

    private void FindInteractibleUI()
    {
        // If reference exists, we're good
        if (interactibleTextGameObject != null)
        {
            return;
        }

        // Try to find by name
        if (!string.IsNullOrEmpty(interactibleTextGameObjectName))
        {
            GameObject found = GameObject.Find(interactibleTextGameObjectName);
            if (found != null)
            {
                interactibleTextGameObject = found;
                return;
            }
        }

        // Try to find InteractibleUI singleton
        InteractibleUI interactibleUI = FindFirstObjectByType<InteractibleUI>();
        if (interactibleUI != null)
        {
            interactibleTextGameObject = interactibleUI.gameObject;
            Debug.Log($"[InteractableController] Found interactible UI GameObject through InteractibleUI component");
            return;
        }
    }

    private GameObject GetInteractibleUI()
    {
        // If reference is null, try to find it again
        if (interactibleTextGameObject == null)
        {
            FindInteractibleUI();
        }

        return interactibleTextGameObject;
    }

    private void OnEnable()
    {
        input.InteractInputEvent += OnInteract;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        input.InteractInputEvent -= OnInteract;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnableMeshes();
        isHidden = false;
        pickUpController.LoseWeapon();
        previousWeaponState = false; // Reset weapon state tracking on scene load
    }

    private void Update()
    {
        DetectInteractable();
        CheckWeaponStateChange();
    }

    // Checks if weapon state has changed and notifies AudioManager when weapon is picked up
    private void CheckWeaponStateChange()
    {
        bool currentWeaponState = hasWeapon;
        
        // Detect when weapon is picked up
        if (!previousWeaponState && currentWeaponState)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.OnWeaponPickedUp();
            }
        }
        
        previousWeaponState = currentWeaponState;
    }

    private void DisableMeshes()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.gameObject.layer = LayerMask.NameToLayer("Hidden");
            meshRenderer.enabled = false;
            playerSightRange.DisablePlayerSightRadius();
        }
    }

    private void EnableMeshes()
    {
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.gameObject.layer = LayerMask.NameToLayer("Player");
            meshRenderer.enabled = true;
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
            PlayerEvents.Instance.PlayerExitHidingSpot();
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

                // Store enemy position before interaction 
                bool wasEnemy = IsEnemyInteractable();
                Vector3? enemyPosition = null;
                if (wasEnemy && killActionData != null)
                {
                    enemyPosition = currentInteractable.transform.position;
                }              

                currentInteractable.Interact();

                // Show kill action text if enemy was killed
                if (wasEnemy)
                {
                    if (killActionData != null && enemyPosition.HasValue && ActionTextManager.Instance != null)
                    {
                        Debug.Log($"[InteractableController] Calling ShowActionText for kill at position: {enemyPosition.Value}");
                        ActionTextManager.Instance.ShowActionText(killActionData, enemyPosition.Value);
                    }
                }
            }
        }
    }

    public void Hide()
    {
        gameObject.layer = LayerMask.NameToLayer("Hidden");
        playerController.DisableMovement();
        isHidden = true;
        DisableMeshes();

        // Disable interactible text UI when hiding
        GameObject interactibleUI = GetInteractibleUI();
        if (interactibleUI != null)
        {
            interactibleUI.SetActive(false);
        }

        // Show hide action text 
        if (hideActionData != null && ActionTextManager.Instance != null)
        {
            Debug.Log($"[InteractableController] Calling ShowActionText for hide (screen center)");
            ActionTextManager.Instance.ShowActionText(hideActionData, null);
        }
    }

    private void StopHiding()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
        playerController.EnableMovement();
        isHidden = false;
        EnableMeshes();

        // Re-enable interactible text UI when stopping hiding
        GameObject interactibleUI = GetInteractibleUI();
        if (interactibleUI != null)
        {
            interactibleUI.SetActive(true);
        }
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