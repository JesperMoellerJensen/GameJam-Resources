using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygenTank : MonoBehaviour {
    public float MaxCapacity;

    public float CurrentCapacity;
    private void Awake() {
        CurrentCapacity = MaxCapacity;
    }

    public bool UseOxygen(float amount) {
        CurrentCapacity = Mathf.Clamp(CurrentCapacity - amount, 0, MaxCapacity);
        return CurrentCapacity > 0;
    }

    //public float RefillOxygen(float amount) {

    //}
}
