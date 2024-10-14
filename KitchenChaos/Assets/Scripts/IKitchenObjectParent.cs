using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this interface to every object that can be parent of a kitchen object
/// </summary>
public interface IKitchenObjectParent
{
    /// <summary>
    /// Check if the parent already has a kitchen object assigned to it
    /// </summary>
    /// <returns></returns>
    public bool HasKitchenObject();

    /// <summary>
    /// Clear the kitchen object assigned to the parent to make it able to take a new kitchen object
    /// </summary>
    public void ClearKitchenObject();

    /// <summary>
    /// Get the transform of the parent
    /// </summary>
    /// <returns></returns>
    public Transform GetKitchenObjectFollowTransform();

    /// <summary>
    /// Assign a kitchen object
    /// </summary>
    /// <param name="ko"></param>
    public void SetKitchenObject(KitchenObject ko);

    /// <summary>
    /// Get the assigned kitchen object
    /// </summary>
    /// <returns></returns>
    public KitchenObject GetKitchenObject();

}
