using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGhost : MonoBehaviour
{
	private World _world;
	private int _sizeX;
	private int _sizeY;

	private GameObject[] _ghosts;
	private void Start()
	{
		_world = FindObjectOfType<World>();

		SetGhostSize(2, 2);
	}
	private void Update()
	{
		CheckArea();
	}

	private void SetGhostSize(int x, int y)
	{
		_sizeX = x;
		_sizeY = y;

		int size = _sizeX * _sizeY;

		_ghosts = new GameObject[size];

		for (int i = 0; i < size; i++)
		{
			GameObject o = new GameObject();
			o.transform.parent = transform;
			o.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/DefaultTile");
			o.GetComponent<SpriteRenderer>().sortingOrder = 1;
			_ghosts[i] = o;
		}
	}

	private void CheckArea()
	{
		Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 Clamp = new Vector2(Mathf.RoundToInt(MousePosition.x), Mathf.RoundToInt(MousePosition.y));

		int i = 0;
		for (int y = 0; y < _sizeY; y++)
		{
			for (int x = 0; x < _sizeX; x++)
			{
				WorldTile tile = _world.GetTile((int)Clamp.x+x, (int)Clamp.y+y);
				_ghosts[i].transform.position = new Vector2((int)Clamp.x + x, (int)Clamp.y + y);

				if (tile.Occupied == false)
				{
					_ghosts[i].GetComponent<SpriteRenderer>().color = Color.green;
				}
				else
				{
					_ghosts[i].GetComponent<SpriteRenderer>().color = Color.red;
				}

				i++;
			}
		}


		//Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Vector2 Clamp = new Vector2(Mathf.RoundToInt(MousePosition.x), Mathf.RoundToInt(MousePosition.y));
		//transform.position = Clamp;

		////WorldTile tile = _world.GetTile((int)Clamp.x, (int)Clamp.y);

		//if (tile.Occupied == false)
		//{
		//	_renderer.color = Color.green;
		//}
		//else
		//{
		//	_renderer.color = Color.red;
		//}
	}
}
