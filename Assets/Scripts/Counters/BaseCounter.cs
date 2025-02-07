using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private Transform attachPoint;

    private KitchenObject kitchenObject;
    public static event EventHandler OnDrop;


    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlt(Player player) {
        Debug.LogError("BaseCounter.InteractAlt();");
    }

    public Transform GetAttachPoint() {
        return attachPoint;
    }

    public void SetAttachPoint(Vector3 position) {
        this.attachPoint.position = position;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null) {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject() {
        SetKitchenObject(null);
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
