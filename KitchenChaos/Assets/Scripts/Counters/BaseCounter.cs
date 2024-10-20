using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField]
    private Transform counterTopPoint;

    private KitchenObject kitchenObject;


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
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        kitchenObject = ko;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public abstract void Interact(Player player);
    public abstract void InteractAlternate(Player player);


}
