using UnityEngine;
using System.Linq;

public class EntityGhost : MonoBehaviour
{
	private World _world;
	private int _sizeX;
	private int _sizeY;

	private GameObject[] _ghosts;
	private WorldTile[] _tiles;
	private Camera _camera;

	private bool _canPlace;

	private void Start()
	{
		_camera = Camera.main;
		_world = FindObjectOfType<World>();

		SetGhostSize(2, 1);
	}

	private void Update()
	{
		CheckArea(new[] { TileType.Grass, TileType.MarsDirt });

		if (Input.GetKeyDown(KeyCode.E) && _canPlace)
		{
			PlaceEntity();
		}
	}

	private void SetGhostSize(int x, int y)
	{
		_sizeX = x;
		_sizeY = y;

		var size = _sizeX * _sizeY;

		_ghosts = new GameObject[size];
		_tiles = new WorldTile[size];


		for (var i = 0; i < size; i++)
		{
			var o = new GameObject();
			o.transform.parent = transform;
			o.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/DefaultTile");
			o.GetComponent<SpriteRenderer>().sortingOrder = 1;
			_ghosts[i] = o;
		}
	}

	private void CheckArea(TileType[] allowedTileTypes)
	{
		Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
		var clamp = new Vector2(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));

		var i = 0;
		for (var y = 0; y < _sizeY; y++)
		{
			for (var x = 0; x < _sizeX; x++)
			{
				var tile = _world.GetTileFromWorldPosition(clamp.x + x, clamp.y + y);
				_ghosts[i].transform.position = new Vector2((int)clamp.x + x, (int)clamp.y + y);

				if (tile.Occupied == false && allowedTileTypes.Contains(tile.TileType))
				{
					_ghosts[i].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
					_canPlace = true;
				}
				else
				{
					_ghosts[i].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
					_canPlace = false;
				}
				_tiles[i] = tile;
				i++;
			}
		}
	}

	private void PlaceEntity()
	{
		foreach (WorldTile tile in _tiles)
		{
			tile.Occupied = true;

			//TODO: Place actual entity
			tile.Sprite = Resources.Load<Sprite>("Sprites/DebugGrey");
		}
	}
}