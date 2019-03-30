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

        if (ItemsInSmelter > 0) {
            isSmelting = true;

            currentSmeltingTime += Time.deltaTime;
            if (currentSmeltingTime >= smeltTimeOfItems[ItemToSmelt.ID]) {
                ItemsInSmelter--;
                currentSmeltingTime = 0;

                if (ItemsInSmelter == 0) {
                    isSmelting = false;
                    ItemToSmelt = null;
                }
            }
        }
    }

    private bool OnAddItemToSmelter(Item itemToSmelt) {
        Debug.Log("hello");
        if (ItemToSmelt != null) {
            return false;
        }


        if (VerifyResource(itemToSmelt)) {
            if (ItemsInSmelter == 0) {
                currentSmeltingTime = 0;
            }

            ItemToSmelt = itemToSmelt;
            ItemsInSmelter++;

            return true;
        }

        return false;
    }

    public override bool Interact(Item item, int amount) {
        Debug.Log("hello1");
        if (RequiredItems.Count == 0) {
            InteractWithEntity(item, amount);
        } else {
            if (RequiredItems.Contains(item)) {
                RequiredItems.Remove(item);
                return true;
            }
        }

        return false;
    }

    private void InteractWithEntity(Item item, int amount) {
        OnAddItemToSmelter(item);
    }


    public bool VerifyResource(Item item) {
        for (int i = 0; i < smeltTimeOfItems.Count; i++) {
            if (smeltTimeOfItems.ContainsKey(item.ID)) {
                return true;
            }
        }

        return false;
    }
}