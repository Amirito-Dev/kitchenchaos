using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    private static Player instance;
    public static Player Instance { get => instance; private set => instance = value; }

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangeEventArgs : EventArgs{
        public BaseCounter selectedCounter;
    }

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private LayerMask counterLayerMask;

    private bool isWalking;

    private Vector3 lastInteractDir;

    private BaseCounter selectedCounter;

    [SerializeField]
    private Transform kitchenObjectHoldPoint;

    private KitchenObject kitchenObject;

    private void Awake() {
        if(Instance != null){
            Debug.Log("There is more than one Player instance.");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
        if(selectedCounter){
            selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }   

    private void HandleInteraction(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Si on est en train de bouger, comme ça quand on s'arrête, on continue de vérifier les objets devant nous
        if(moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, lastInteractDir, out raycastHit, interactDistance, counterLayerMask)){
            // Debug.Log(raycastHit.transform);
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                if(baseCounter != selectedCounter){
                    SetSelectedCounter(baseCounter);
                }
            }else{
                selectedCounter = null;
                SetSelectedCounter(null);
            }
        }
        else{
            // Debug.Log("-");
            selectedCounter = null;
            SetSelectedCounter(null);
        }

    }

    public bool IsWalking(){
        return isWalking;
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // Si on peut pas bouger tout droit, on test pour en x,y (x,z)
        if(!canMove){
            // Vérifier si on peut bouger en X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            // Si on peut que bouger en X
            if(canMove){
                moveDir = moveDirX;
            }
            // Sinon on vérifie peut que bouger en Z (Y)
            else{
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                // Si on peut que bouger en Z
                if(canMove){
                    moveDir = moveDirZ;
                }
                // Sinon on peut bouger dans aucune direction
                else{

                }
            }
        }

        if (canMove){
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangeEventArgs{
            selectedCounter = selectedCounter
        });
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        kitchenObject = ko;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

}
