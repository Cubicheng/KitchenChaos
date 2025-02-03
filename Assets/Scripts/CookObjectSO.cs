using UnityEngine;
[CreateAssetMenu()]
public class CookObjectSO : ScriptableObject{
    public KitchenObjectSO Input;
    public KitchenObjectSO Output;
    public float cookingTime;
}
