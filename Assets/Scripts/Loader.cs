using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {
    public enum Scene {
        MainMenu,
        Loading,
        GameScene
    };

    private static Scene targetScene;

    public static void LoadScene(Scene targetScene) {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene((int)Scene.Loading);
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene((int)targetScene);
    }
}
