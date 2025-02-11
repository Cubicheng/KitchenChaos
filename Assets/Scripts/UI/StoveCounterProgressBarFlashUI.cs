using UnityEngine;

public class StoveCounterProgressBarFlashUI : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;

    private const string IS_WARNING = "IsWarning";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        stoveCounter.OnWarningHide += StoveCounter_OnWarningHide;
        stoveCounter.OnWarningShow += StoveCounter_OnWarningShow;
    }

    private void StoveCounter_OnWarningHide(object sender, System.EventArgs e) {
        animator.SetBool(IS_WARNING, false);
    }

    private void StoveCounter_OnWarningShow(object sender, System.EventArgs e) {
        animator.SetBool(IS_WARNING, true);
    }
}
