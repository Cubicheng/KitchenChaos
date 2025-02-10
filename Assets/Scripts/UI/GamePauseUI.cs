using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    public static GamePauseUI instance { get; private set; }

    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button OptionsButton;

    private void Awake() {
        instance = this;
        ResumeButton.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        MainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            SceneManager.LoadScene((int)Loader.Scene.MainMenu);
        });
        OptionsButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.instance.Show();
        });
    }

    private void Start() {
        Hide();
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    public void Show() {
        ResumeButton.Select();
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
