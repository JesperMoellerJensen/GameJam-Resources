using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerOxygenTank))]
public class Player : MonoBehaviour {
    public float MaxHealth;
    public float OxygenTickSpeed;
    [FormerlySerializedAs("OxygenConsumptionn")] public float OxygenConsumption;
    public float NoOxygenDamage;

    private PlayerOxygenTank _oxygenTank;
    private SpriteRenderer _spriteRenderer;
    private float _currentHealth;


    private void Awake() {
        _currentHealth = MaxHealth;
    }
    private void Start() {
        _oxygenTank = GetComponent<PlayerOxygenTank>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        InvokeRepeating(nameof(UseOxygen), 0, OxygenTickSpeed);
    }

    public void TakeDamage(float amount) {
        _currentHealth -= amount;
        //Debug.Log($"I took {amount} damage, i now have {_currentHealth} health left");
        if (_currentHealth <= 0) {
            Death();
        }
    }

    private void UseOxygen() {
        if (_oxygenTank.UseOxygen(OxygenConsumption)) {
            //Debug.Log($"I can breathe, and i have {_oxygenTank.CurrentCapacity}");
        } else {
            //Debug.Log("I CANT BREATHE!");
            TakeDamage(NoOxygenDamage);
        }
    }

    private void Death() {
        //Debug.Log("I Am Dead");
    }

    private void OnGUI() {

        GUI.Box(new Rect(10, 10, 120, 100),"Stats");
        GUI.Label(new Rect(20, 30, 100, 20), "Health");
        GUI.Label(new Rect(100, 30, 100, 20), _currentHealth.ToString());
        GUI.Label(new Rect(20, 60, 100, 20), "OxygenTank");
        GUI.Label(new Rect(100, 60, 100, 20), _oxygenTank.CurrentCapacity.ToString());
    }
}
