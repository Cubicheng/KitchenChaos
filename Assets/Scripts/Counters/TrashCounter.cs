using System;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public static event EventHandler OnTranshed;
    new public static void ResetStaticData() {
        OnTranshed = null;
    }
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
            OnTranshed?.Invoke(this, EventArgs.Empty);
        }
    }
}
