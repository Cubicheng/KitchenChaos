using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlateCounter : BaseCounter{

    public event EventHandler OnPlateAdded;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateObjectSO;

    private const int MAX_PLATE_NUM = 4;
    private const float INCREASE_DELTA = 4.0f;

    private int plateNum;
    private float duration;

    private void Start() {
        duration = 0;
        plateNum = 0;
    }

    private void Update() {
        if (plateNum == MAX_PLATE_NUM) {
            return;
        }
        duration += Time.deltaTime;
        if (duration > INCREASE_DELTA) {
            AddPlate();
        }
    }

    private void AddPlate() {
        plateNum++;
        duration = 0;
        OnPlateAdded?.Invoke(this, EventArgs.Empty);
    }

    private void RemovePlate() {
        plateNum--;
        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player) {
        if (plateNum == 0) {
            return;
        }
        if (player.HasKitchenObject()) {
            return;
        }
        RemovePlate();
        KitchenObject.SpawnKitchenObject(plateObjectSO, player);
    }

    public override void InteractAlt(Player player) {
    }

}
