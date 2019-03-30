using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemPickup : MonoBehaviour {
    public Item item;
    private void Start() {
        GetComponent<SpriteRenderer>().sprite = item.Image;
    }
}
