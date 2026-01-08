using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Inputs.IPlayerMovementActions
{


    // Event that are triggered when input activity is detected

    public event Action<Vector2> MoveInputEvent;

    private Inputs inputs;

    void Awake()
    {
        try
        {
            inputs = new Inputs();
            inputs.PlayerMovement.SetCallbacks(this);
            inputs.PlayerMovement.Enable();
        }
        catch (Exception exception)
        {
            Debug.LogError("Error " + exception.Message);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInputEvent?.Invoke(context.ReadValue<Vector2>());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
