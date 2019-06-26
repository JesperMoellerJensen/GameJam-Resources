using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldTile : MonoBehaviour {
    private Sprite _sprite;

    public Vector2 Position;
    public TileType TileType;

    [NonSerialized] public bool Occupied;
    private static float _s = World.WorldSize;
    private readonly int _x = Mathf.RoundToInt(_s / 2);
    private readonly int _y = Mathf.RoundToInt(_s / 2);
    private Transform _playerTransform;
    private GameObject _entity;

    public Sprite Sprite {
        get { return _sprite; }
        set {
            _sprite = value;
            GetComponent<SpriteRenderer>().sprite = _sprite;
        }
    }

    public GameObject Entity {
        get { return _entity; }
        set {
            _entity = value;
            Occupied = value != null;
        }
    }

    private void Start() {
        //Sprite = Resources.Load<Sprite>("Sprites/DebugMarsDirt");
        DebugSetRandomTileType();
        _playerTransform = GameObject.FindWithTag("Player").transform;
        //InvokeRepeating("UpdatePosition", 0, 2);
    }

    float Dist(float a, float b) {
        return b - a;
    }

    private void UpdatePosition() {
        var playerPosition = _playerTransform.position;
        var px = playerPosition.x;
        var py = playerPosition.y;
        if (Dist(px, transform.position.x) > _x) {
            transform.Translate(new Vector3(-_s, 0, 0));
        } else if (Dist(px, transform.position.x) < -_x) {
            transform.Translate(new Vector3(_s, 0, 0));
        }

        if (py - transform.position.y < -_y) {
            transform.Translate(new Vector3(0, -_s, 0));
        } else if (py - transform.position.y > _y) {
            transform.Translate(new Vector3(0, _s, 0));
        }
    }

    //TODO: Remove this
    //------ DEBUG -------
    private void DebugSetRandomTileType() {

        if (Random.Range(0, 100) < 160) {
            Sprite = Resources.Load<Sprite>("Sprites/Tiles/MarsGround_" + Random.Range(0, 8));
            TileType = TileType.MarsDirt;
        } else {
            Sprite = Resources.Load<Sprite>("Sprites/Debug/DebugGrass");
            TileType = TileType.Grass;
        }

        if (Random.Range(0, 100) < 1) {
            var e = Instantiate(Resources.Load<GameObject>("Prefabs/CarbonOre"));
            e.transform.position = transform.position;
            Entity = e;
        } else if (Random.Range(0, 100) < 1) {
            var e = Instantiate(Resources.Load<GameObject>("Prefabs/IronOre"));
            e.transform.position = transform.position;
            Entity = e;
        }
    }
}

public enum TileType {
    MarsDirt = 0,
    Grass
}

