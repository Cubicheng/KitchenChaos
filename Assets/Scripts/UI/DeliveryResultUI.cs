using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour {
    private const string POP = "Pop";

    [SerializeField] Image background;
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Sprite acceptSprite;
    [SerializeField] Sprite rejectSprite;
    [SerializeField] Color acceptColor;
    [SerializeField] Color rejectColor;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.instance.OnRecipeAccepted += DeliveryManager_OnRecipeAccepted;
        DeliveryManager.instance.OnRecipeRejected += DeliveryManager_OnRecipeRejected;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeAccepted(object sender, EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POP);
        background.color = acceptColor;
        iconImage.sprite = acceptSprite;
        text.text = "DELIVERY\nSUCCESS";
    }

    private void DeliveryManager_OnRecipeRejected(object sender, EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POP);
        background.color = rejectColor;
        iconImage.sprite = rejectSprite;
        text.text = "DELIVERY\nFAILURE";
    }
}
