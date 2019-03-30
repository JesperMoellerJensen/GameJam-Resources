using System;
using System.Collections.Generic;
using UnityEngine;

public class SmelterBehavior : MonoBehaviour {
    public static Func<GameObject, Item, bool> AddItemToSmelter;

    public Item ItemToSmelt;
    public int ItemsInSmelter;

    [SerializeField]
    private bool isSmelting = false;
    private float currentSmeltingTime = 0;

    private Dictionary<ItemID, float> smeltTimeOfItems;


    // Start is called before the first frame update
    void Start() {
        ItemToSmelt = null;
        ItemsInSmelter = 0;

        smeltTimeOfItems = new Dictionary<ItemID, float>()
        {
            {ItemID.CopperOre, 5f},
            {ItemID.IronOre, 5f},
            {ItemID.GoldOre, 5f}
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

    private void OnEnable() {
        AddItemToSmelter += OnAddItemToSmelter;
    }

    private void OnDisable() {
        AddItemToSmelter -= OnAddItemToSmelter;
    }

    private bool OnAddItemToSmelter(GameObject smelter, Item itemToSmelt) {
        if (ItemToSmelt != null) {
            return false;
        }

        if (smelter == this.gameObject) {
            if (VerifyResource(itemToSmelt)) {
                if (ItemsInSmelter == 0) {
                    currentSmeltingTime = 0;
                }

                ItemToSmelt = itemToSmelt;
                ItemsInSmelter++;

                return true;
            }
        }

        return false;
    }

    private void OnRemoveItemsInSmelter() {

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
