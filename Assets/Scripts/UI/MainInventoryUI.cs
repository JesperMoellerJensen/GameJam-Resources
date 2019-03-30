using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventoryUI : MonoBehaviour
{
    public static Action<Dictionary<int, ItemSlot>> UpdateItems;

    private GameObject ItemSlotPrefab;

    void Start() {
        ItemSlotPrefab = (GameObject)Resources.Load("Prefabs/UI/ItemSlot");
    }

    void OnEnable() {
        UpdateItems += OnUpdateItems;
    }

    void OnDestroy() {
        UpdateItems -= OnUpdateItems;
    }

    private void OnUpdateItems(Dictionary<int, ItemSlot> itemSlots) {

        for (int i = 0; i < itemSlots.Count; i++) {

            GameObject slot = Instantiate(ItemSlotPrefab);
            slot.transform.position = new Vector3(i * 100f, 0,0);
        }
    }
}
