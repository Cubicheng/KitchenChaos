using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler<RecipeEventArgs> OnRecipeAdd;
    public event EventHandler<RecipeEventArgs> OnRecipeRemove;
    public event EventHandler OnRecipeAccepted;
    public event EventHandler OnRecipeRejected;

    public class RecipeEventArgs : EventArgs {
        public RecipeSO recipeSO;
    }

    public static DeliveryManager instance { get; private set; }

    [SerializeField] private RecipeListSO recipeList;

    private List<RecipeSO> waitingRecipeSOList;
    private const float DELTA_TIME = 8f;
    private const int WAITING_LIST_MAX = 4;
    private float duration;

    private void Awake() {
        instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Start() {
        duration = DELTA_TIME;
    }

    private void Update() {
        duration += Time.deltaTime;
        if (duration < DELTA_TIME || waitingRecipeSOList.Count == WAITING_LIST_MAX) {
            return;
        }
        duration = 0;
        AddRecipe();
    }

    private void AddRecipe() {
        RecipeSO recipeSO = recipeList.recipeSOList[UnityEngine.Random.Range(0, recipeList.recipeSOList.Count)];
        waitingRecipeSOList.Add(recipeSO);
        OnRecipeAdd?.Invoke(this, new RecipeEventArgs() { recipeSO = recipeSO });
    }
    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        if (plateKitchenObject == null) {
            OnRecipeRejected?.Invoke(this, new RecipeEventArgs());
            return false;
        }
        foreach (RecipeSO recipeSO in waitingRecipeSOList) {
            if (MatchRecipe(plateKitchenObject, recipeSO)) {
                waitingRecipeSOList.Remove(recipeSO);
                OnRecipeRemove?.Invoke(this, new RecipeEventArgs() { recipeSO = recipeSO });
                OnRecipeAccepted?.Invoke(this, new RecipeEventArgs());
                return true;
            }
        }
        OnRecipeRejected?.Invoke(this, new RecipeEventArgs());
        return false;
    }

    private bool MatchRecipe(PlateKitchenObject plateKitchenObject, RecipeSO recipeSO) {
        if(plateKitchenObject.GetCount() != recipeSO.kitchenObjectList.Count) {
            return false;
        }
        for (int i = 0; i < plateKitchenObject.GetCount(); i++) {
            KitchenObjectSO kitchenObjectSO = plateKitchenObject.GetKitchenObjectSO(i);
            if (!recipeSO.kitchenObjectList.Contains(kitchenObjectSO)) {
                return false;
            }
        }
        return true;
    }
}
