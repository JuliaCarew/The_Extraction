using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Inputs.IPlayerMovementActions
{


    // Event that are triggered when input activity is detected

    public event Action<Vector2> MoveInputEvent;
    public event Action<InputAction.CallbackContext> InteractInputEvent;
    public event Action<InputAction.CallbackContext> PauseInputEvent;
    public event Action ChangeCameraInputEvent; 


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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractInputEvent?.Invoke(context);
        }
    }    

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // change state to pasued 
            PauseInputEvent?.Invoke(context);
        }
    }

    public void OnChangeCamera(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            ChangeCameraInputEvent?.Invoke(); 
        }
    }
}
