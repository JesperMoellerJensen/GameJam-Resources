using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour {
    private Sprite _sprite;

    public Vector2 Position;

    public bool Occupied;
    private static float _s = World.WORLD_SIZE;
    private readonly int _x = Mathf.RoundToInt(_s / 2);
    private readonly int _y = Mathf.RoundToInt(_s / 2);
    private Transform _playerTransform;

    public Sprite Sprite {
        get { return _sprite; }
        set {
            _sprite = value;
            GetComponent<SpriteRenderer>().sprite = _sprite;
        }
    }

    private void Start() {
        Sprite = Resources.Load<Sprite>("Sprites/DebugSquare");
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    float dist(float a, float b) {
        return b - a;
    }

    private void Update() {
        var playerPosition = _playerTransform.position;
        var px = playerPosition.x - _s / 2;
        var py = playerPosition.y - _s / 2;
        if (dist(px, transform.position.x) > _x) {
            transform.Translate(new Vector3(-_s, 0, 0));
        } else if (dist(px, transform.position.x) < -_x) {
            transform.Translate(new Vector3(_s, 0, 0));
        }

        if (py - transform.position.y < -_y) {
            transform.Translate(new Vector3(0, -_s, 0));
        } else if (py - transform.position.y > _y) {
            transform.Translate(new Vector3(0, _s, 0));
        }
    }
}