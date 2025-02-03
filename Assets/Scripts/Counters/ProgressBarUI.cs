using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start() {
        cuttingCounter.OnProgressBarChanged += CuttingCounter_OnProgressBarChanged;
        progressBarImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnProgressBarChanged(object sender, CuttingCounter.OnProgressBarChangedEventArgs e) {
        progressBarImage.fillAmount = e.progress;
        if(gameObject.activeSelf == false) {
            if (e.progress != 1) {
                Show();
            }
        } else {
            if (e.progress == 1) {
                Hide();
            }
        }
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
