using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetAttachPoint();

    public KitchenObject GetKitchenObject();
    public void SetKitchenObject(KitchenObject kitchenObject);

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
