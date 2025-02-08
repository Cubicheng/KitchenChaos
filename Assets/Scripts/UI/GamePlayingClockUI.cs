using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour {

    [SerializeField] private Image clock;

    private void Update() {
        if (!KitchenGameManager.Instance.IsGamePlaying()) {
            return;
        }
        clock.fillAmount = KitchenGameManager.Instance.GetProgress();
    }
}
