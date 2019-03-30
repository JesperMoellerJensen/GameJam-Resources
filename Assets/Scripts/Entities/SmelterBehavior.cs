using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterBehavior : EntityBehavior {
    public Item ItemToSmelt;
    public int ItemsInSmelter;
    public List<Item> RequiredItems;

    [SerializeField] private bool isSmelting = false;
    private float currentSmeltingTime = 0;

    private Dictionary<ItemID, float> smeltTimeOfItems;

    // Start is called before the first frame update
    void Start() {
        ItemToSmelt = null;
        ItemsInSmelter = 0;

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

        if (ItemsInSmelter <= 0) return;
        isSmelting = true;

        currentSmeltingTime += Time.deltaTime;
        if (!(currentSmeltingTime >= smeltTimeOfItems[ItemToSmelt.ID])) return;
        ItemsInSmelter--;
        currentSmeltingTime = 0;

        if (ItemsInSmelter != 0) return;
        isSmelting = false;
        ItemToSmelt = null;
    }

    private void OnAddItemToSmelter(MouseInteract mouseInteract) {
        if (mouseInteract.SelectedItem == null) {
            RemoveFromSmelter(mouseInteract, ItemToSmelt, ItemsInSmelter);
            return;
        }

        if (CanBeSmelted(mouseInteract.SelectedItem) == false) {
            return;
        }

        if (ItemToSmelt == null) {
            AddToSmelter(mouseInteract);
            return;
        }

        if (ItemToSmelt.ID == mouseInteract.SelectedItem.ID) {
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
        var t2 = ItemsInSmelter;
        AddToSmelter(mouseInteract);
        RemoveFromSmelter(mouseInteract, t, t2);
    }

    private void AddSameToSmelter(MouseInteract mouseInteract) {
        ItemsInSmelter += mouseInteract.ItemStack;
        mouseInteract.ItemStack = 0;
        mouseInteract.SelectedItem = null;
    }

    private void AddToSmelter(MouseInteract mouseInteract) {
        ItemToSmelt = mouseInteract.SelectedItem;
        ItemsInSmelter = mouseInteract.ItemStack;
        mouseInteract.SelectedItem = null;
        mouseInteract.ItemStack = 0;
    }

    private void RemoveFromSmelter(MouseInteract mouseInteract, Item itemToSmelt, int itemsInSmelter) {
        mouseInteract.SelectedItem = itemToSmelt;
        mouseInteract.ItemStack = itemsInSmelter;
        ItemToSmelt = null;
        ItemsInSmelter = 0;
    }

    public override void Interact(MouseInteract mouseInteract) {
        Debug.Log("hello1");
        if (RequiredItems.Count == 0) {
            OnAddItemToSmelter(mouseInteract);
        } else {
            if (RequiredItems.Contains(mouseInteract.SelectedItem)) {
                RequiredItems.Remove(mouseInteract.SelectedItem);
            }
        }
    }
}