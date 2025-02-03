using UnityEngine;

public class LookAt : MonoBehaviour {

    private enum Mode {
        LookAt,
        LookAtInvert,
        CameraForward,
        CameraForwardInvert
    };

    [SerializeField] private Mode mode;

    private void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInvert:
                Vector3 dirFromCamera = -Camera.main.transform.position + transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInvert:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
