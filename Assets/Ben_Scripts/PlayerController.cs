using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;

    public float movSpeed = 4f;
    public Rigidbody rb;
    private bool canMove = true; 

    // Interaction Variable
    [SerializeField] private BaseInteractable bInteractable = null;
    public float rayDistance = 3f;
    public LayerMask interactableLayer;

    // Rotation variables
    [Header("Rotation")]
    public float rotSpeed = 0.1f;
    public float turnSmoothTime = 0.1f;

    // Input Variables

    public Vector2 moveInput; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DetectingInteractable(out RaycastHit hit)) 
        {
            if(bInteractable == null) 
            {
                try
                {
                    bInteractable = hit.collider.GetComponent<BaseInteractable>();                    
                }
                catch (System.Exception e) 
                {
                    Debug.Log("No Base interactable found.  " + e); 
                }
            }
        }
        else 
        {
            if(bInteractable != null) 
            {
                bInteractable = null; 
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            ApplyMovement();
        }
    }

    public void ApplyMovement() 
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        // No input → no movement or rotation
        if (moveDirection.sqrMagnitude < 0.01f)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        moveDirection.Normalize();

        // Move
        rb.linearVelocity = moveDirection * movSpeed;

        // Rotate to face movement direction
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.MoveRotation(Quaternion.Slerp(
            rb.rotation,
            targetRotation,
            rotSpeed
        ));
    }

    void SetMoveInput(Vector2 inputVector)
    {
        moveInput = new Vector2(inputVector.x, inputVector.y);
    }


    public void HideInObject() 
    {
        //canMove = false;
        this.gameObject.layer = LayerMask.NameToLayer("Hidden");
    }

    void InteractWithObject(InputAction.CallbackContext c) 
    {
        // TODO:  Logic for interacting with objects
        Debug.Log("Interact"); 

        if(bInteractable != null) 
        {
            bInteractable.Interact(); 
        }
    }

    private bool DetectingInteractable(out RaycastHit hit) 
    {
        return Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, interactableLayer);
    }

     
    private void OnEnable()
    {
        inputManager.MoveInputEvent += SetMoveInput; 
        inputManager.InteractInputEvent += InteractWithObject; 
    }

    private void OnDisable()
    {
        inputManager.MoveInputEvent -= SetMoveInput; 
        inputManager.InteractInputEvent -= InteractWithObject;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,
            transform.position + transform.forward * rayDistance);
    }


}
