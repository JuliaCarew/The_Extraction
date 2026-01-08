using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;

    public float movSpeed = 4f;
    public Rigidbody rb; 

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
        //Get input direction
        Vector3 moveInputDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 worldMoveDirection = transform.TransformDirection(moveInputDirection);

        rb.linearVelocity = worldMoveDirection * movSpeed;


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
