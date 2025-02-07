using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public static DeliveryCounter instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            DeliveryManager.instance.DeliverRecipe(null);
            return;
        }
        player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject);
        if (DeliveryManager.instance.DeliverRecipe(plateKitchenObject)) {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
