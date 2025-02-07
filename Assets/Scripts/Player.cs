using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent {
    public static Player Instance { get; private set; }

    public event EventHandler OnPickup;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform attachPoint;

    private bool isWalking = false;
    private Vector3 lastInteractDir;

    private KitchenObject kitchenObject;

    private const float PLAYER_RADIUS = 0.65f;
    private const float PLAYER_HEIGHT = 2f;
    private const float INTERACT_DISTANCE = 2f;
    private BaseCounter selectedCounter;

    private void Awake() {
        if (Instance != null) {
            Debug.Log("more than one instance");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAltAction += GameInput_OnInteractAltAction;
    }

    private void GameInput_OnInteractAltAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.InteractAlt(this);
        }
    }
        
    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVector2Normalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDirection != Vector3.zero) {
            lastInteractDir = moveDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, INTERACT_DISTANCE, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVector2Normalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDirection != Vector3.zero) {
            isWalking = true;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        } else {
            isWalking = false;
        }

        if (!CanMove(moveDirection)) {
            moveDirection = new Vector3(inputVector.x, 0, 0);
            if (!CanMove(moveDirection)) {
                moveDirection = new Vector3(0, 0, inputVector.y);
            }
        }

        if (CanMove(moveDirection)) {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
    }

    private bool CanMove(Vector3 moveDirection) {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PLAYER_HEIGHT, PLAYER_RADIUS, moveDirection, moveSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCoutner) {
        this.selectedCounter = selectedCoutner;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs() { selectedCounter = selectedCounter });
    }

    public Transform GetAttachPoint() {
        return attachPoint;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null) {
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject() {
        SetKitchenObject(null);
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
