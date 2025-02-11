using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;

    private void Awake() {
        PlayButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.Scene.GameScene);
        });

        QuitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        if (Gamepad.current != null) {
            PlayButton.Select();
        }
    }
}
