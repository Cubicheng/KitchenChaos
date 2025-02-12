using TMPro;
using UnityEngine;

public class GameScoreUI : MonoBehaviour {
    private const string Pop = "Pop";
    private Animator animator;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() {
        animator = GetComponent<Animator>();
        DeliveryManager.instance.OnScoreIncreased += DeliveryManager_OnScoreIncreased;
    }

    private void DeliveryManager_OnScoreIncreased(object sender, System.EventArgs e) {
        scoreText.text = DeliveryManager.instance.GetScore().ToString();
        animator.SetTrigger(Pop);
    }
}
