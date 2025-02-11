using UnityEngine;

public class BurnWarningUI : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnWarningHide += StoveCounter_OnWarningHide;
        stoveCounter.OnWarningShow += StoveCounter_OnWarningShow;
        Hide();
    }

    private void StoveCounter_OnWarningHide(object sender, System.EventArgs e) {
        Hide();
    }

    private void StoveCounter_OnWarningShow(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {  
        gameObject.SetActive(false);
    }
}
