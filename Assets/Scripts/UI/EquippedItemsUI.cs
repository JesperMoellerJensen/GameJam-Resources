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
        ChangeSelectedSlot(0);
    }

    void OnEnable() {
        InventoryHandler.OnChangedSelectedSlot += ChangeSelectedSlot;
        InventoryHandler.OnChangedEquippedItem += ChangeEquippedItem;
    }

    void OnDestroy() {
        InventoryHandler.OnChangedSelectedSlot -= ChangeSelectedSlot;
        InventoryHandler.OnChangedEquippedItem -= ChangeEquippedItem;
    }

    void ChangeSelectedSlot(int index) {
        SelectedSlot.transform.position = slots[index].transform.position;
    }

    void ChangeEquippedItem(int index, ItemSlot item) {
        


    }

    void GenerateItemSlots(int inventorySize) {
        float startPos = inventorySize / 2 - inventorySize;
        startPos += inventorySize % 2 == 0 ? 0.5f : 0;

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
