using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    private PlayerInputAction playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        if(OnInteractAction != null){
            OnInteractAction(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovementVectorNormalized(){

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;

        // Debug.Log(inputVector);

        return inputVector;
    }
}
