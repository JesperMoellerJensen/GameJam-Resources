using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SmelterBehavior : EntityBehavior {
    public ItemSlot ItemToSmelt;
    public List<Item> RequiredItems;

    [SerializeField] private bool isSmelting = false;
    private float currentSmeltingTime = 0;

    private Dictionary<ItemID, float> smeltTimeOfItems;

    // Start is called before the first frame update
    void Start() {
        ItemToSmelt = null;

        smeltTimeOfItems = new Dictionary<ItemID, float>() {
            {ItemID.CopperNugget, 5f},
            {ItemID.IronNugget, 5f},
            {ItemID.GoldNugget, 5f}
        };
    }

    // Update is called once per frame
    void Update() {
        if (ItemToSmelt == null) {
            return;
        }

        if (ItemToSmelt.StackSize <= 0) return;

        isSmelting = true;

        currentSmeltingTime += Time.deltaTime;
        if (!(currentSmeltingTime >= smeltTimeOfItems[ItemToSmelt.Item.ID])) return;
        Debug.Log($"{ItemToSmelt.StackSize}");
        SpawnBar();
        ItemToSmelt.StackSize--;
        currentSmeltingTime = 0;

        if (ItemToSmelt.StackSize != 0) return;
        isSmelting = false;
        ItemToSmelt = null;
    }

    public void SpawnBar() {
        GameObject item = Instantiate(Resources.Load<GameObject>("Prefabs/Item"),transform.position + new Vector3(0,-1,0),transform.rotation);
        item.GetComponent<ItemPickup>().item = Resources.Load<Item>($"Scriptable Objects/{ItemToSmelt.Item.ResourceType}Bar");
        item.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5,5),Random.Range(-2f,-6f));

    }

    private void OnAddItemToSmelter(MouseInteract mouseInteract) {
        if (mouseInteract.SelectedItemSlot == null) {
            RemoveFromSmelter(mouseInteract, ItemToSmelt);
            return;
        }

        if (CanBeSmelted(mouseInteract.SelectedItemSlot.Item) == false) {
            return;
        }

        if (ItemToSmelt == null) {
            AddToSmelter(mouseInteract);
            return;
        }

        if (ItemToSmelt.Item.ID == mouseInteract.SelectedItemSlot.Item.ID) {
            AddSameToSmelter(mouseInteract);
            return;
        }

        Replace(mouseInteract);
    }

    private bool CanBeSmelted(Item selectedItem) {
        return smeltTimeOfItems.ContainsKey(selectedItem.ID);
    }

    private void Replace(MouseInteract mouseInteract) {
        var t = ItemToSmelt;
        AddToSmelter(mouseInteract);
        RemoveFromSmelter(mouseInteract, t);
    }

    private void AddSameToSmelter(MouseInteract mouseInteract) {
        ItemToSmelt.StackSize += mouseInteract.SelectedItemSlot.StackSize;
        mouseInteract.SelectedItemSlot = null;
    }

    private void AddToSmelter(MouseInteract mouseInteract) {
        ItemToSmelt = mouseInteract.SelectedItemSlot;
        mouseInteract.SelectedItemSlot = null;
    }

    private void RemoveFromSmelter(MouseInteract mouseInteract, ItemSlot itemToSmelt) {
        mouseInteract.SelectedItemSlot = itemToSmelt;
        ItemToSmelt = null;
    }

    public override void Interact(MouseInteract mouseInteract) {
        Debug.Log(mouseInteract.SelectedItemSlot.Item.DisplayName + " " + mouseInteract.SelectedItemSlot.StackSize + " In smelter");
        if (RequiredItems.Count == 0) {
            OnAddItemToSmelter(mouseInteract);
        } else {
            if (RequiredItems.Contains(mouseInteract.SelectedItemSlot.Item)) {
                RequiredItems.Remove(mouseInteract.SelectedItemSlot.Item);
            }
        }
    }
}