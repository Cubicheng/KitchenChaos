using UnityEngine;
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
    }
}
