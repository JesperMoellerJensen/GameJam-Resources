using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SmelterBehavior : EntityBehavior {
    public ItemSlot ItemSlot;
    public List<Item> RequiredItems;

    [SerializeField] private bool isSmelting = false;
    private float currentSmeltingTime = 0;

    private Dictionary<ItemID, float> smeltTimeOfItems;
    private TextMesh _textMesh;

    // Start is called before the first frame update
    void Start() {
        ItemSlot = null;

        smeltTimeOfItems = new Dictionary<ItemID, float>() {
            {ItemID.CopperNugget, 5f},
            {ItemID.IronNugget, 5f},
            {ItemID.GoldNugget, 5f}
        };

        _textMesh = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update() {
        if (ItemSlot == null) {
            return;
        }

        isSmelting = true;

        currentSmeltingTime += Time.deltaTime;
        if (currentSmeltingTime >= smeltTimeOfItems[ItemSlot.Item.ID]) {
            Debug.Log($"{ItemSlot.StackSize}");
            SpawnBar();
            ItemSlot.StackSize--;
            currentSmeltingTime = 0;

            if (ItemSlot.StackSize == 0) {
                isSmelting = false;
                ItemSlot = null;
            }
        }

        UpdateText();
    }

    public void SpawnBar() {
        GameObject item = Instantiate(Resources.Load<GameObject>("Prefabs/Item"), transform.position + new Vector3(0, -1, 0), transform.rotation);
        item.GetComponent<ItemPickup>().item = Resources.Load<Item>($"Scriptable Objects/{ItemSlot.Item.ResourceType}Bar");
        item.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5, 5), Random.Range(-2f, -6f));

    }

    private bool CanBeSmelted(Item selectedItem) {
        return smeltTimeOfItems.ContainsKey(selectedItem.ID);
    }

    private void Switch(ItemSlot selectedItem) {
        var t = ItemSlot;
        ItemSlot = selectedItem;
        selectedItem = t;
    }

    public override ItemSlot Interact(ItemSlot selectedItem) {

        // If entity has been builded
        if (RequiredItems.Count == 0) {

            // If mouse item is null, then take content of smelter
            if (selectedItem == null) {
                currentSmeltingTime = 0;
                var temp = ItemSlot;
                ItemSlot = null;
                return temp;
            }

            if (CanBeSmelted(selectedItem.Item)) {

                if (ItemSlot == null) {
                    ItemSlot = new ItemSlot(selectedItem.Item, selectedItem.StackSize);
                    return null;
                }

                // If the item can be smelted and is already smelting, add to stack. Else switch content.
                if (ItemSlot.Item.ID == selectedItem.Item.ID) {
                    ItemSlot.StackSize += selectedItem.StackSize;
                    return null;
                }

                // Switch items
                currentSmeltingTime = 0;
                var temp = ItemSlot;
                ItemSlot = selectedItem;
                return temp;
            }

            return selectedItem;

        } else {
            if (RequiredItems.Contains(selectedItem.Item)) {
                RequiredItems.Remove(selectedItem.Item);
            }
        }

        return null;
    }

    private void UpdateText() {
        float smeltTime = 0;
        smeltTimeOfItems.TryGetValue(ItemSlot.Item.ID, out smeltTime);
        float percentage = Mathf.RoundToInt(currentSmeltingTime / smeltTime * 100f);
        _textMesh.text = $"{ItemSlot.StackSize} {ItemSlot.Item.DisplayName} | {percentage}%";
    }
}