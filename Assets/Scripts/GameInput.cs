using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAltAction;

    private PlayerInputActions playerInputActions;
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Interact_Alt.performed += Interact_Alt_performed;

    }

    private void Interact_Alt_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAltAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector2Normalized() {
        return playerInputActions.Player.Move.ReadValue<Vector2>();
    }
}
