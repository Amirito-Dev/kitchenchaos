using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputAction playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;   
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        if (OnInteractAlternateAction != null)
        {
            OnInteractAlternateAction(this, EventArgs.Empty);
        }
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        if(OnInteractAction != null){
            OnInteractAction(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovementVectorNormalized(){

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
