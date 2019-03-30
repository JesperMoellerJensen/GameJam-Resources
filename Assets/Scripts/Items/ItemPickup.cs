using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickup : MonoBehaviour {
    public Item item;
    private void Start() {
        GetComponent<SpriteRenderer>().sprite = item.Image;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        InventoryHandler.AddItem?.Invoke(item, 1);
        Destroy(gameObject);
    }
}
