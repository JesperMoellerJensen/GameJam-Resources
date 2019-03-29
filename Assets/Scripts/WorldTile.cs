using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
	private Sprite _sprite;

	public Vector2 Position;
	public TileType TileType;

	public bool Occupied;

	public Sprite Sprite
	{
		get { return _sprite; }
		set
		{
			_sprite = value;
			GetComponent<SpriteRenderer>().sprite = _sprite;
		}
	}

	private void Start()
	{
		//Sprite = Resources.Load<Sprite>("Sprites/DebugMarsDirt");
		DebugSetRandomTileType();
	}

	//TODO: Remove this
	//------ DEBUG -------
	private void DebugSetRandomTileType()
	{

		if(Random.Range(0, 100) < 50)
		{
			Sprite = Resources.Load<Sprite>("Sprites/DebugMarsDirt");
			TileType = TileType.MarsDirt;
		}
		else
		{
			Sprite = Resources.Load<Sprite>("Sprites/DebugGrass");
			TileType = TileType.Grass;
		}
	}
}

public enum TileType
{
	MarsDirt = 0,
	Grass
}

