using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    public override void InteractAlternate(Player player)
    {
    }

    public override void Interact(Player player){
        if(!HasKitchenObject()) { 
            // If the player already has a kitchen object, put it on the counter
            if(player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            // Give the kitchen object to the player if he not already has a kitchen object
            if(!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                Debug.Log(string.Format("{0} is already on the counter !", GetKitchenObject().KitchenObjectSO.name));
            }
        }
    }
}
