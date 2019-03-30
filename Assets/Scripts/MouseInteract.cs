using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteract : MonoBehaviour {

    public Item SelectedItem;
    public int ItemStack;
    public LayerMask LayerMask;

    private void Update() {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DebugText();

        if (Input.GetButtonDown("Fire2")) {
            EntityClicked();
        }
    }

    private void EntityClicked() {
        if (SelectedItem != null) {
           
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
