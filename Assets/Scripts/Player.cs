using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerOxygenTank))]
public class Player : MonoBehaviour {
    public float MaxHealth;
    public float OxygenTickSpeed;
    public float OxygenConsumptionn;
    public float NoOxygenDamage;

    private PlayerOxygenTank _oxygenTank;
    private float _currentHealth;


    private void Awake() {
        _currentHealth = MaxHealth;
    }
    private void Start() {
        _oxygenTank = GetComponent<PlayerOxygenTank>();

        InvokeRepeating("UseOxygen", 0, OxygenTickSpeed);
    }

    public void TakeDamage(float amount) {
        _currentHealth -= amount;
        Debug.Log($"I took {amount} damage, i now have {_currentHealth} health left");
        if(_currentHealth <= 0) {
            Death();
        }
    }

    private void UseOxygen() {
        if (_oxygenTank.UseOxygen(OxygenConsumptionn)) {
            Debug.Log($"I can breathe, and i have {_oxygenTank.CurrentCapacity}");
        } else {
            Debug.Log("I CANT BREATHE!");
            TakeDamage(NoOxygenDamage);
        }
    }

    private void Death() {
        Debug.Log("I Am Dead");
    }

    

}
