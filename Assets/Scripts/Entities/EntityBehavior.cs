using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EntityBehavior : MonoBehaviour {
    private Transform _player;
    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        SetSortingOrder();
    }

    private void SetSortingOrder() {

        int offset = Mathf.FloorToInt(transform.position.y + 0.5f - _player.position.y);

        if (offset > 0) {
            _spriteRenderer.sortingOrder = -1;
        } else {
            _spriteRenderer.sortingOrder = 1;
        }
    }
}
