using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlateCounterVisual : MonoBehaviour{
    [SerializeField] private KitchenObjectSO plateObjectSO;
    [SerializeField] private PlateCounter plateCounter;
    private const float offset_y = 0.1f;

    private List<GameObject> plateList;
    private void Awake() {
        plateList = new List<GameObject>();
    }

    private void Start() {
        plateCounter.OnPlateAdded += PlateCounter_OnPlateAdded;
        plateCounter.OnPlateRemoved+= PlateCounter_OnPlateRemoved;
    }
    private void PlateCounter_OnPlateAdded(object sender, System.EventArgs e) {
        Transform newPlate = KitchenObject.SpawnKitchenObject(plateObjectSO, plateCounter, offset_y * plateList.Count);
        plateList.Add(newPlate.gameObject);
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        GameObject plate = plateList[plateList.Count - 1];
        plateList.Remove(plate.gameObject);
        Destroy(plate);
    }

}
