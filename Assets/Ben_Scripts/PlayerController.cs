using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;

    public float movSpeed = 4f;
    public Rigidbody rb;


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
        
    }

    private void FixedUpdate()
    {       
        ApplyMovement(); 
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

    void InteractWithObject(InputAction.CallbackContext c) 
    {
        // TODO:  Logic for interacting with objects
        Debug.Log("Interact"); 
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


}
