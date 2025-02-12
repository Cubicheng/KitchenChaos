using System;
using UnityEngine;

public class ContainerCouner : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbed;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbed?.Invoke(this, EventArgs.Empty);
            return;
        }
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
            if (plateKitchenObject.TryAddIngredient(kitchenObjectSO)) {
                OnPlayerGrabbed?.Invoke(this, EventArgs.Empty);
            }
            return;
        }
    }
}
