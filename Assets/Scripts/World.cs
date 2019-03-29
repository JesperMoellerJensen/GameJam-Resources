using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    private WorldTile[,] _tileMap;

    public static int WORLD_SIZE = 10;
    public GameObject TilePrefab;

    private void Start() {
        _tileMap = new WorldTile[WORLD_SIZE, WORLD_SIZE];
        InitializeTiles();
    }


    private void InitializeTiles() {
        for (int y = 0; y < WORLD_SIZE; y++) {
            for (int x = 0; x < WORLD_SIZE; x++) {
                _tileMap[x, y] = Instantiate(TilePrefab, new Vector2(x, y), transform.rotation, transform)
                    .GetComponent<WorldTile>();
            }
        }
    }

    public WorldTile GetTile(int x, int y) {
        return _tileMap[x, y];
    }

    public WorldTile GetTileFromWorldPosition(float x, float y) {
        var x2 = Mathf.RoundToInt(x).Mod(WORLD_SIZE);
        var y2 = Mathf.RoundToInt(y).Mod(WORLD_SIZE);
        return _tileMap[x2, y2];
    }
}