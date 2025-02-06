using UnityEngine;
using UnityEngine.UI;

public class RecipeIconTemplate : MonoBehaviour {
    [SerializeField] private Image icon;

    public void Init(Sprite sprite) {
        icon.gameObject.SetActive(true);
        icon.sprite = sprite;
    }
}
