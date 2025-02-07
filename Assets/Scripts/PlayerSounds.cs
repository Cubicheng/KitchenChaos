using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    private const float DELTA_TIME = 0.15f;
    private float duration;
    private void Awake() {
        duration = 0;
    }

    private void Update() {
        if (!Player.Instance.IsWalking()) {
            return;
        }
        duration += Time.deltaTime;
        if (duration > DELTA_TIME) {
            duration = 0;
            SoundManager.instance.PlayFootstepSound();
        }
    }

}
