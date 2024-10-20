using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // Throw the kitchen object
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }

    public override void InteractAlternate(Player player)
    {
        // Nothing to do
    }
}
