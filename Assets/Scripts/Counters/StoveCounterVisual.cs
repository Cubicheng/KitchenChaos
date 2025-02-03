using UnityEngine;

public class StoveCounterVisual : MonoBehaviour{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject Particle;
    [SerializeField] private GameObject StoveOnVisual;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        if (e.state == StoveCounter.State.Idle) {
            Hide();
        } else {
            Show();
        }
    }

    private void Hide() {
        Particle.SetActive(false);
        StoveOnVisual.SetActive(false);
    }

    private void Show() {
        Particle.SetActive(true);
        StoveOnVisual.SetActive(true);
    }
}
