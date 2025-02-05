using System;
using UnityEngine;
using static CuttingCounter;
using static IProgressBarParent;

public class StoveCounter : BaseCounter, IProgressBarParent {

    public event EventHandler<OnProgressBarChangedEventArgs> OnProgressBarChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    [SerializeField] private CookObjectSO[] cookObjectSOs;
    private float cookDuration;

    private const string UncookedName = "MeatPattyUncooked";

    public enum State {
        Idle,
        Cooking,
        Burning
    }

    private State state = State.Idle;

    private void Update() {
        switch (state) {
            case State.Idle:
                if (GetCookOutput(GetKitchenObject()) != null) {
                    cookDuration = 0;
                    if (GetKitchenObject().GetKitchenObjectSO().name == UncookedName) {
                        SetState(State.Cooking);
                    }else {
                        SetState(State.Burning);
                    }
                }
                break;
            case State.Cooking:
                if (!HasKitchenObject()) {
                    SetState(State.Idle);
                    SetProgressBar(1);
                    break;
                }

                cookDuration += Time.deltaTime;
                if (cookDuration>= GetMaxCookTime(GetKitchenObject())) {
                    cookDuration = 0;

                    KitchenObjectSO kitchenObjectSO = GetCookOutput(GetKitchenObject());
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
                    SetState(State.Burning);
                }
                SetProgressBar(cookDuration / GetMaxCookTime(GetKitchenObject()));
                break;
            case State.Burning:
                if (!HasKitchenObject()) {
                    SetState(State.Idle);
                    SetProgressBar(1);
                    break;
                }

                cookDuration += Time.deltaTime;
                SetProgressBar(cookDuration / GetMaxCookTime(GetKitchenObject()));
                if (cookDuration >= GetMaxCookTime(GetKitchenObject())) {
                    KitchenObjectSO kitchenObjectSO = GetCookOutput(GetKitchenObject());
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
                    SetState(State.Idle);
                }
                break;
        }
    }

    private void SetProgressBar(float progress) {
        OnProgressBarChanged?.Invoke(this, new OnProgressBarChangedEventArgs { progress = progress });
    }

    private void SetState(State state) {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = this.state });
    }
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (GetMaxCookTime(player.GetKitchenObject()) != -1) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cookDuration = 0;
                }
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }
    public override void InteractAlt(Player player) {

    }

    private KitchenObjectSO GetCookOutput(KitchenObject kitchenObject) {
        foreach (CookObjectSO cookObject in cookObjectSOs) {
            if (cookObject.Input == kitchenObject.GetKitchenObjectSO()) {
                return cookObject.Output;
            }
        }
        return null;
    }
    private float GetMaxCookTime(KitchenObject kitchenObject) {
        foreach (CookObjectSO cookObject in cookObjectSOs) {
            if (cookObject.Input == kitchenObject.GetKitchenObjectSO()) {
                return cookObject.cookingTime;
            }
        }
        return -1;
    }
}
