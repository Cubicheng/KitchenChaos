using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs: EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjects;

    private List<KitchenObjectSO> kitchenObjectSOs;

    private void Awake() {
        kitchenObjectSOs = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!validKitchenObjects.Contains(kitchenObjectSO)) {
            return false;
        }
        if (kitchenObjectSOs.Contains(kitchenObjectSO)) {
            return false;
        }
        kitchenObjectSOs.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs() { kitchenObjectSO = kitchenObjectSO });
        return true;
    }

}
