using UnityEngine;

public class MouseInteract : MonoBehaviour {
    private Camera _camera;
    private EntityGhost _entityGhost;

    public Item SelectedItem;
    public int ItemStack;
    public LayerMask LayerMask;
    public World World;

    private void Start() {
        _entityGhost = GetComponent<EntityGhost>();
        _camera = Camera.main;
    }

    private void Update() {
        if (_entityGhost.BuildMode) {
            return;
        }

        transform.position = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition);
        DebugText();

        if (Input.GetButtonDown("Fire1")) {
            var position = transform.position;
            var tile = World.GetTileFromWorldPosition(position.x, position.y);
            if (tile.Entity != null) tile.Entity.GetComponent<EntityBehavior>().Interact(this);
        }
    }


    //TODO: Remove
    private void DebugText() {
        if (SelectedItem != null) {
            GetComponentInChildren<TextMesh>().text = SelectedItem.DisplayName;
        } else {
            GetComponentInChildren<TextMesh>().text = "";
        }
    }
}