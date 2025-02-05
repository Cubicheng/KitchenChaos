using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlateCompleteVisual : MonoBehaviour{

    [Serializable]
    public struct Ingredient{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject kitchenObjectSOVisual;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<Ingredient> ingredients;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach(Ingredient ingredient in ingredients) {
            if (ingredient.kitchenObjectSO == e.kitchenObjectSO) {
                ingredient.kitchenObjectSOVisual.SetActive(true);
                break;
            }
        }
    }
}
