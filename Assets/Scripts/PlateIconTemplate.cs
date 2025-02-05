using UnityEngine;
using UnityEngine.UI;

public class PlateIconTemplate : MonoBehaviour {

    [SerializeField] private Image sprite;
    [SerializeField] private Image background;

    public void SetSprite(KitchenObjectSO kitchenObjectSO) {
        sprite.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        sprite.sprite = kitchenObjectSO.sprite;
    }
    
}
