using System;
using UnityEngine;

public class World : MonoBehaviour {
    private WorldTile[,] _tileMap;

    public const int WorldSize = 100;
    public GameObject TilePrefab;

    private void Start() {
        _tileMap = new WorldTile[WorldSize, WorldSize];
        InitializeTiles();
    }


    private void InitializeTiles() {
        for (var y = 0; y < WorldSize; y++) {
            for (var x = 0; x < WorldSize; x++) {
                var transform1 = transform;
                _tileMap[x, y] = Instantiate(TilePrefab, new Vector2(x, y), transform1.rotation, transform1)
                    .GetComponent<WorldTile>();
                _tileMap[x, y].GetComponent<SpriteRenderer>().sortingOrder = -10;
            }
        }
    }

    public WorldTile GetTile(int x, int y) {
        return _tileMap[x, y];
    }

    public WorldTile GetTileFromWorldPosition(float x, float y) {
        var i = X2(x, y);
        return _tileMap[i.x, i.y];
    }

    private static Vector2Int X2(float x, float y) {
        var x2 = Mathf.RoundToInt(x).Mod(WorldSize);
        var y2 = Mathf.RoundToInt(y).Mod(WorldSize);
        return new Vector2Int(x2, y2);
    }
}