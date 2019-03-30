using UnityEngine;

public class WorldTile : MonoBehaviour {
    private Sprite _sprite;

	public Vector2 Position;
	public TileType TileType;

    public bool Occupied;
    private static float _s = World.WorldSize;
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

	private void Start()
	{
		//Sprite = Resources.Load<Sprite>("Sprites/DebugMarsDirt");
		DebugSetRandomTileType();
		 _playerTransform = GameObject.FindWithTag("Player").transform;
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

public enum TileType
{
	MarsDirt = 0,
	Grass
}

