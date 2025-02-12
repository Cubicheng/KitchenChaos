using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button RetryButton;
    [SerializeField] private Button MainMenuButton;

    private void Awake() {
        RetryButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.Scene.GameScene);
        });
        MainMenuButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.Scene.MainMenu);
        });
    }

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsGameOver()) {
            scoreText.text = DeliveryManager.instance.GetScore().ToString();
            Show();
        }
    }

    private void Show() {
        if (Gamepad.current != null) {
            RetryButton.Select();
        } else {
            EventSystem.current.SetSelectedGameObject(null);
        }
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
