using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
	private Sprite _sprite;

	public Vector2 Position { get; set; }
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
		Sprite = Resources.Load<Sprite>("Sprites/DefaultTile");
	}
}
