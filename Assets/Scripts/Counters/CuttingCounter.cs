using System;
using UnityEngine;
using static IProgressBarParent;

public class CuttingCounter : BaseCounter, IProgressBarParent {

    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData() {
        OnAnyCut = null;
    }

    public event EventHandler<OnProgressBarChangedEventArgs> OnProgressBarChanged;

    public event EventHandler OnPlayerCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingCnt;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) { 
                if (GetMaxCuttingCnt(player.GetKitchenObject()) != -1) {
                    cuttingCnt = 0;
                    OnProgressBarChanged?.Invoke(this, new OnProgressBarChangedEventArgs() { progress = 0 });
                }
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
                cuttingCnt = 1;
                OnProgressBarChanged?.Invoke(this, new OnProgressBarChangedEventArgs() { progress = 1 });
            } else {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
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
        GameInput.Instance.GamepadVibrate(0.5f, 0.5f, 0.1f);
        OnPlayerCut?.Invoke(this, EventArgs.Empty);
        OnAnyCut?.Invoke(this, EventArgs.Empty);
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
