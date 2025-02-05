using UnityEngine;

public class PlateIconUI : MonoBehaviour{

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private PlateIconTemplate plateIconTemplate;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        Transform newIcon = Instantiate(plateIconTemplate.transform, transform);
        newIcon.GetComponent<PlateIconTemplate>().SetSprite(e.kitchenObjectSO);
    }
}
