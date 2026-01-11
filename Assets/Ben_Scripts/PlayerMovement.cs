using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;

    public float movSpeed = 4f;
    public Rigidbody rb;

    private bool canMove = true;

    // Rotation variables
    [Header("Rotation")]
    public float rotSpeed = 0.1f;
    public float turnSmoothTime = 0.1f;

    // Input Variables
    public Vector2 moveInput; 
 

    private void FixedUpdate()
    {
        if (canMove)
        {
            ApplyMovement();
        }
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
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



     
    private void OnEnable()
    {
        inputManager.MoveInputEvent += SetMoveInput;  
    }

    private void OnDisable()
    {
        inputManager.MoveInputEvent -= SetMoveInput; 
    }
}
