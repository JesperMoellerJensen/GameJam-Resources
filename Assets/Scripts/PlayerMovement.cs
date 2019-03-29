using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float Speed;
	private Vector2 _moveDir;

	private void Update()
	{
		_moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * Speed * Time.deltaTime;
		Move();
	}

	private void Move()
	{
		transform.Translate(_moveDir);
	}
}
