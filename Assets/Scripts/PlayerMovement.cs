using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour {
    private Vector2 _moveDir;
    private Rigidbody2D _rb;
    private Animator _animator;

    public float Speed;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update() {
        _moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * Speed;
        _rb.velocity = _moveDir;
        _animator.SetFloat("Speed", Mathf.Abs(_moveDir.magnitude));

        if (_moveDir.x < 0) {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        } else if (_moveDir.x > 0) {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }
}
