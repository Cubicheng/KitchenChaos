using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private GameObject container;
    [SerializeField] private RecipeTemplate recipeTemplate;

    private List<RecipeTemplate> recipes;

    private void Awake() {
        recipes = new List<RecipeTemplate>();
    }

    private void Start() {
        DeliveryManager.instance.OnRecipeAdd += DeliveryManager_OnRecipeAdd;
        DeliveryManager.instance.OnRecipeRemove += DeliveryManager_OnRecipeRemove;
    }
    private void DeliveryManager_OnRecipeAdd(object sender, DeliveryManager.RecipeEventArgs e) {
        RecipeTemplate recipe = Instantiate(recipeTemplate, container.transform);
        recipe.GetComponent<RecipeTemplate>().Init(e.recipeSO);
        recipes.Add(recipe);
    }

    private void DeliveryManager_OnRecipeRemove(object sender, DeliveryManager.RecipeEventArgs e) {
        foreach(RecipeTemplate recipeIconTemplate in recipes) {
            if (recipeIconTemplate.GetRecipeSO() == e.recipeSO) {
                recipes.Remove(recipeIconTemplate);
                Destroy(recipeIconTemplate.gameObject);
                break;
            }
        }
    }
}
