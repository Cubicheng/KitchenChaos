using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private Transform attachPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlt(Player player) {
        Debug.LogError("BaseCounter.InteractAlt();");
    }

    public Transform GetAttachPoint() {
        return attachPoint;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject() {
        SetKitchenObject(null);
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
