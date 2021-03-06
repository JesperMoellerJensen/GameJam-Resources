using UnityEngine;
using System.Linq;

public class EntityGhost : MonoBehaviour {
    private World _world;
    private int _sizeX;
    private int _sizeY;

    private GameObject[] _ghosts;
    private WorldTile[] _tiles;
    private Camera _camera;

    private GameObject _entityType;
    private Vector3 _entityOffset;

    private bool _canPlace;
    public bool BuildMode;

    private void Start() {
        _camera = Camera.main;
        _world = FindObjectOfType<World>();
        SetGhostSize(2, 1);
        _entityOffset = new Vector3(0.5f, 0.5f);
        _entityType = Resources.Load<GameObject>("Prefabs/Smelter");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            BuildMode = !BuildMode;
        }


        if (!BuildMode) {
            CleanUpGhosts();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            CleanUpGhosts();
            SetGhostSize(2, 1);
            _entityType = Resources.Load<GameObject>("Prefabs/Smelter");
            _entityOffset = new Vector3(0.5f, 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            CleanUpGhosts();
            SetGhostSize(3, 2);
            _entityType = Resources.Load<GameObject>("Prefabs/OxygenBank");
            _entityOffset = new Vector3(1f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            CleanUpGhosts();
            SetGhostSize(1, 1);
            _entityType = Resources.Load<GameObject>("Prefabs/IronOre");
            _entityOffset = new Vector3(0,0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            CleanUpGhosts();
            SetGhostSize(1, 1);
            _entityType = Resources.Load<GameObject>("Prefabs/CarbonOre");
            _entityOffset = new Vector3(0, 0);
        }

        if (_ghosts == null) {
            InitGhostTiles();
        }
        CheckArea(new[] { TileType.MarsDirt });

        if (_ghosts == null) {
            InitGhostTiles();
        }
        CheckArea(new[] { TileType.MarsDirt });

        if (Input.GetButtonDown("Fire1") && _canPlace) {
            PlaceEntity();
        }
    }

    private void CleanUpGhosts() {
        if (_ghosts != null) {
            foreach (var ghost in _ghosts) {
                Destroy(ghost);
            }
            _ghosts = null;

        }
    }

    private void SetGhostSize(int x, int y) {
        _sizeX = x;
        _sizeY = y;

        InitGhostTiles();
    }

    private void InitGhostTiles() {
        var size = _sizeX * _sizeY;

        _ghosts = new GameObject[size];
        _tiles = new WorldTile[size];


        for (var i = 0; i < size; i++) {
            var o = new GameObject();
            o.transform.parent = transform;
            o.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Debug/DefaultTile");
            o.GetComponent<SpriteRenderer>().sortingOrder = 1;
            _ghosts[i] = o;
        }
    }

    private void CheckArea(TileType[] allowedTileTypes) {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        var clamp = new Vector2(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));

        _canPlace = true;
        var i = 0;
        for (var y = 0; y < _sizeY; y++) {
            for (var x = 0; x < _sizeX; x++) {
                var tile = _world.GetTileFromWorldPosition(clamp.x + x, clamp.y + y);
                _ghosts[i].transform.position = new Vector3((int)clamp.x + x, (int)clamp.y + y, 0);

                var spriteRenderer = _ghosts[i].GetComponent<SpriteRenderer>();
                if (tile.Occupied == false && allowedTileTypes.Contains(tile.TileType)) {
                    spriteRenderer.color = new Color(0, 1, 0, 0.5f);
                } else {
                    spriteRenderer.color = new Color(1, 0, 0, 0.5f);
                    _canPlace = false;
                }
                _tiles[i] = tile;
                i++;
            }
        }
    }

    private void PlaceEntity() {
        var e = Instantiate(_entityType);
        e.transform.position = _tiles[0].transform.position + _entityOffset;
        foreach (WorldTile tile in _tiles) {
            tile.Entity = e;
        }

    }
}