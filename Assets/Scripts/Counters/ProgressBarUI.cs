using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private GameObject hasIProgressBarParentObject;

    private IProgressBarParent progressBarParent;

    private void Start() {
        progressBarParent = hasIProgressBarParentObject.GetComponent<IProgressBarParent>();
        if(progressBarParent == null) {
            Debug.LogError("progressBarParent == null");
        }
        progressBarParent.OnProgressBarChanged += progressBarParent_OnProgressBarChanged;
        progressBarImage.fillAmount = 0;
        Hide();
    }

    private void progressBarParent_OnProgressBarChanged(object sender, IProgressBarParent.OnProgressBarChangedEventArgs e) {
        progressBarImage.fillAmount = e.progress;
        if(gameObject.activeSelf == false) {
            if (e.progress != 1) {
                Show();
            }
        } else {
            if (e.progress >= 1) {
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
