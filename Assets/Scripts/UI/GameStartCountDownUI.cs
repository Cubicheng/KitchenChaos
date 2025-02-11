using System;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countDownText;
    private const string NUMBER_POP = "NumberPop";
    private Animator animator;

    private int num = 3;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        KitchenGameManager.Instance.OnCntChanged += KitchenGameManager_OnCntChanged;
    }

    private void KitchenGameManager_OnCntChanged(object sender, EventArgs e) {
        countDownText.text = num.ToString();
        animator.SetTrigger(NUMBER_POP);
        SoundManager.instance.PlayWarningSound();
        num--;
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e) {
        KitchenGameManager.State state = KitchenGameManager.Instance.GetState();
        switch (state) {
            case KitchenGameManager.State.CountingDown:
                Show();
                break;
            case KitchenGameManager.State.GamePlaying:
                Hide();
                break;
        }
    }

    private void Show() {
        countDownText.gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
