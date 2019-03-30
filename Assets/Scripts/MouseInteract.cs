using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteract : MonoBehaviour {

    public Item SelectedItem;
    public int ItemStack;
    public LayerMask LayerMask;
    public GameObject EntityHovered;

    private void Update() {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetEntityHovered();
        DebugText();

        if (Input.GetButtonDown("Fire2")) {
            EntityClicked();
        }
    }
    void GetEntityHovered() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerMask);

        if (hit.collider != null) {
            EntityHovered = hit.collider.transform.root.gameObject;

        } else {
            EntityHovered = null;
        }
    }

    private void EntityClicked() {
        if (EntityHovered != null) {

            if (SelectedItem != null)
                EventManager.TriggerFuncEvent("AddItemToEntity", new object[] { EntityHovered, SelectedItem, ItemStack });
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
