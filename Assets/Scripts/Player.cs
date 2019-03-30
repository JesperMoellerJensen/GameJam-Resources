using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float MaxHealth;

	private PlayerOxygenTank _oxygenTank;
	private float _currentHealth;

	private void Start()
	{
		InvokeRepeating(UseOxygen)
	}

	private void UseOxygen()
	{

	}

}
