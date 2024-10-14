using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO KitchenObjectSO { get => kitchenObjectSO; }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
    {
        // Remove the kitchen object from the parent (because the parent will not contain the kitchen object anymore)
        if(kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject();
        }

        kitchenObjectParent = newKitchenObjectParent;

        // If the counter not already contain a kitchen object
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError(string.Format("IKitchenObjectParent already has a kitchenObject!"));
        }

        // Assign the kitchen object to this counter
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();

        // Place it to (0, 0, 0) relatively to the parent
        transform.localPosition = Vector3.zero;
    }

}
