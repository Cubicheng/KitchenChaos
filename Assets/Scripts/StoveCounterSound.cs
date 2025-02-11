using UnityEngine;

public class StoveCounterSound : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private bool isWarning = false;
    private const float WARNING_DELTA = 0.2f;
    private float duration = 0;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnWarningShow += StoveCounter_OnWarningShow;
        stoveCounter.OnWarningHide += StoveCounter_OnWarningHide;
    }

    private void StoveCounter_OnWarningShow(object sender, System.EventArgs e) {
        isWarning = true;
    }

    private void StoveCounter_OnWarningHide(object sender, System.EventArgs e) {
        isWarning = false;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        if (e.state == StoveCounter.State.Idle) {
            audioSource.Pause();
        } else {
            audioSource.Play();
        }
    }

    private void Update() {
        if (!isWarning) {
            return;
        }
        duration+= Time.deltaTime;
        if (duration > WARNING_DELTA) {
            duration = 0;
            SoundManager.instance.PlayWarningSound();
        }
    }
}
