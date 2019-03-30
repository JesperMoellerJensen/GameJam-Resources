using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
	private Vector2 _moveDir;
    private Rigidbody2D _rb;
   
	public float Speed;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
	{
		_moveDir = Speed * Time.deltaTime * new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
		Move();
    }

	private void Move()
	{
        _rb.velocity = _moveDir;
	}
}
