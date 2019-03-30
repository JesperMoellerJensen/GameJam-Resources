using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsUI : MonoBehaviour
{
    public static Action<int> UpdateItems;

    public GameObject ItemSlotPrefab;
    public GameObject SelectedSlot;

    private List<GameObject> slots;

    void Start() {

        GenerateItemSlots(InventoryHandler.EquippedInventorySize);
        //ChangeSelectedSlot(0);
    }

    void OnEnable() {
        InventoryHandler.OnChangedSelectedSlot += ChangeSelectedSlot;
        InventoryHandler.OnChangedItem += ChangeEquippedItem;
    }

    void OnDestroy() {
        InventoryHandler.OnChangedSelectedSlot -= ChangeSelectedSlot;
        InventoryHandler.OnChangedItem -= ChangeEquippedItem;
    }

    void ChangeSelectedSlot(ItemSlot itemSlot) {
        //SelectedSlot.transform.position = slots[index].transform.position;
    }

    void ChangeEquippedItem(int index, ItemSlot itemSlot) {

        if (index < InventoryHandler.EquippedInventorySize) {
            GameObject slot = slots[index];
            slot.GetComponent<SpriteRenderer>().sprite = itemSlot?.Item.Image;
            slot.GetComponentInChildren<TextMesh>().text = itemSlot.StackSize > 1 ? itemSlot.StackSize + "" : "";
        }

    }

    void GenerateItemSlots(int inventorySize) {
        float startPos = inventorySize / 2 - inventorySize;
        startPos += inventorySize % 2 == 0 ? 0.5f : 1f;

        slots = new List<GameObject>();
        for (int i = 0; i < inventorySize; i++) {

            GameObject itemSlot = (GameObject)Instantiate(Resources.Load("Prefabs/UI/ItemSlot"));
            itemSlot.transform.parent = transform;
            itemSlot.transform.localPosition = new Vector2(startPos + i, 0);
            itemSlot.transform.localScale = Vector3.one;

            slots.Add(itemSlot);
        }
    }
}
