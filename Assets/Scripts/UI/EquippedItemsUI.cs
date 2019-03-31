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

    void Awake() {

        GenerateItemSlots(InventoryHandler.EquippedInventorySize);
    }

    void OnEnable() {
        InventoryHandler.OnChangedSelectedSlot += ChangeSelectedSlot;
        InventoryHandler.UpdatedInventory += UpdateInventory;
    }

    void OnDestroy() {
        InventoryHandler.OnChangedSelectedSlot -= ChangeSelectedSlot;
        InventoryHandler.UpdatedInventory -= UpdateInventory;
    }

    void ChangeSelectedSlot(int index, ItemSlot itemSlot) {
        if (index < InventoryHandler.EquippedInventorySize) {
            SelectedSlot.transform.position = slots[index].transform.position;
        }
        else {
            SelectedSlot.transform.Translate(new Vector3(0, 0, -100));
        }
    }

    void UpdateInventory(List<ItemSlot> inventory) {

        for (int i = 0; i < InventoryHandler.EquippedInventorySize; i++) {
            GameObject slot = slots[i];


            if(inventory[i] != null) {
                slot.GetComponent<SpriteRenderer>().sprite = inventory[i].Item.Image;
                slot.GetComponentInChildren<TextMesh>().text = inventory[i].StackSize > 1 ? inventory[i].StackSize + "" : "";
            }
            else {
                slot.GetComponent<SpriteRenderer>().sprite = null;
                slot.GetComponentInChildren<TextMesh>().text = "";
            }
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
