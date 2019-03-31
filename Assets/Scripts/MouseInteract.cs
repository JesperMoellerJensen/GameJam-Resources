using System;
using UnityEngine;

public class MouseInteract : MonoBehaviour {
    private Camera _camera;
    private EntityGhost _entityGhost;

    public ItemSlot SelectedItemSlot {
        get { return InventoryHandler.GetSelectedSlot?.Invoke(); }
        set {
            InventoryHandler.UpdateSelectedSlot?.Invoke(value);
        }
    }
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
            if (tile.Entity != null) {
                SelectedItemSlot = tile.Entity.GetComponent<EntityBehavior>().Interact(SelectedItemSlot);
            }
        }

        if (Input.GetButtonDown("Fire2")) {
            var position = transform.position;
            var tile = World.GetTileFromWorldPosition(position.x, position.y);
            if (tile.Entity != null) {
               
                // TODO: Interact with only 5
            }
        }
    }

    private void OnEnable() {
        InventoryHandler.OnChangedSelectedSlot += ChangeSelectedItemSlot;
    }
    private void OnDisable() {
        InventoryHandler.OnChangedSelectedSlot -= ChangeSelectedItemSlot;
    }

    private void ChangeSelectedItemSlot(int index, ItemSlot itemSlot) {
        SelectedItemSlot = itemSlot;
    }

    //TODO: Remove
    private void DebugText() {
        if (SelectedItemSlot != null) {
            GetComponentInChildren<TextMesh>().text = $"{SelectedItemSlot.StackSize}x{SelectedItemSlot.Item.DisplayName}";
        } else {
            GetComponentInChildren<TextMesh>().text = "";
        }
    }
}