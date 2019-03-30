using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float Speed;
	private Vector2 _moveDir;

	private void Update()
	{
		_moveDir = Speed * Time.deltaTime * new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
		Move();
    }

	private void Move()
	{
		transform.Translate(_moveDir);
	}
}
