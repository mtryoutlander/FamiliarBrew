using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnSprintAction;
    public event EventHandler OnSprintEndAction;
    public event EventHandler OnInteractAltAction;
    public event EventHandler OnThrow;

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Sprint.performed += Sprint_performed;
        playerInputActions.Player.Sprint.canceled += Sprint_end;
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAltenate.performed += InteractAlt_performed;
        playerInputActions.Player.Throw.performed += Throw_performed;
    }

    private void Throw_performed(InputAction.CallbackContext context)
    {
        OnThrow?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlt_performed(InputAction.CallbackContext context)
    {
        OnInteractAltAction?.Invoke(this, EventArgs.Empty);
    }

    private void Sprint_end(InputAction.CallbackContext obj)
    {
        OnSprintEndAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Sprint_performed(InputAction.CallbackContext obj)
    {
        OnSprintAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementVectorNormal()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        Vector3 moveDir;

        moveDir = (new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime);
        if (inputVector == Vector2.zero)
        {
            moveDir = Vector3.zero;
        }

        return moveDir;

    }

}
