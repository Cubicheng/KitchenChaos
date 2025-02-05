using UnityEngine;

public class KitchenObject : MonoBehaviour{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent, float offset_y = 0) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;
        this.kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetAttachPoint();
        transform.localPosition = new Vector3(0, offset_y, 0);
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if(this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        } else {
            plateKitchenObject = null;
            return false;
        }
    }

    public static Transform SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent, float offset_y = 0) {
        Transform kitchenObject = Instantiate(kitchenObjectSO.prefab);
        kitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(kitchenObjectParent, offset_y);
        return kitchenObject;
    }

}
