using UnityEngine;

public class ContainerCounterVIsual : MonoBehaviour
{

    [SerializeField] private ContainerCouner containerCouner;
    private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containerCouner.OnPlayerGrabbed += ContainerCouner_OnPlayerGrabbed;
    }

    private void ContainerCouner_OnPlayerGrabbed(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
