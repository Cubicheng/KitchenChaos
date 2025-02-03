using System;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    public event EventHandler<OnProgressBarChangedEventArgs> OnProgressBarChanged;
    public class OnProgressBarChangedEventArgs : EventArgs {
        public float progress;
    }

    public event EventHandler OnPlayerCut;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingCnt;

    public override void Interact(Player player) {
        Debug.Log(player.HasKitchenObject());
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                if (GetMaxCuttingCnt(GetKitchenObject()) != -1) {
                    cuttingCnt = 0;
                    OnProgressBarChanged?.Invoke(this, new OnProgressBarChangedEventArgs() { progress = 0 });
                }
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlt(Player player) {
        if (!HasKitchenObject()) {
            return;
        }
        
        KitchenObjectSO cuttingObject = GetSlicedOutput(GetKitchenObject());
        if (cuttingObject == null) {
            return;
        }
        int MaxCuttingCnt = GetMaxCuttingCnt(GetKitchenObject());
        cuttingCnt++;
        OnPlayerCut?.Invoke(this, EventArgs.Empty);
        OnProgressBarChanged?.Invoke(this, new OnProgressBarChangedEventArgs() { progress = (float)cuttingCnt / (float)MaxCuttingCnt });
        if (cuttingCnt < MaxCuttingCnt) {
            return;
        }
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(cuttingObject, this);
    }
    private KitchenObjectSO GetSlicedOutput(KitchenObject kitchenObject) {
        foreach (CuttingRecipeSO cuttingRecipeObject in cuttingRecipeSOs) {
            if (cuttingRecipeObject.Input == kitchenObject.GetKitchenObjectSO()) {
                return cuttingRecipeObject.Output;
            }
        }
        return null;
    }

    private int GetMaxCuttingCnt(KitchenObject kitchenObject) {
        foreach (CuttingRecipeSO cuttingRecipeObject in cuttingRecipeSOs) {
            if (cuttingRecipeObject.Input == kitchenObject.GetKitchenObjectSO()) {
                return cuttingRecipeObject.cuttingTime;
            }
        }
        return -1;
    }
}
