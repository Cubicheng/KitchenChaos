using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplate : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private GameObject iconContainer;
    [SerializeField] private Image background;
    [SerializeField] private RecipeIconTemplate iconTemplate;
    private RecipeSO recipeSO;

    public RecipeSO GetRecipeSO() {
        return recipeSO;
    }

    public void Init(RecipeSO recipeSO) {
        this.recipeSO = recipeSO;
        SetRecipeName(recipeSO.recipeName);
        AddIcons(recipeSO);
        Show();
    }
 
    private void Show() {
        recipeName.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        iconContainer.SetActive(true);
    }

    private void SetRecipeName(string recipeName) {
        this.recipeName.text = recipeName;
    }

    private void AddIcons(RecipeSO recipeSO) {
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectList) {
            RecipeIconTemplate icon = Instantiate(iconTemplate, iconContainer.transform);
            icon.Init(kitchenObjectSO.sprite);
        }
    }
    
}
