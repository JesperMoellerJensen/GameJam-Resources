using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public static Action<Item, int> AddItem;
    public static Action<int, int> RemoveItems;
    public static Func<int, Item> GetItem;
    public static Action<int, int> MoveItem;

    public int Capacity = 32;

    private Dictionary<int, ItemSlot> Inventoy;


    // Start is called before the first frame update
    void Start()
    {
        Inventoy = new Dictionary<int, ItemSlot>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        AddItem += OnAddItem;
        RemoveItems += OnRemoveItems;
        GetItem += OnGetItem;
        MoveItem += OnMoveItem;
    }

    private void OnDestroy() {
        AddItem -= OnAddItem;
        RemoveItems -= OnRemoveItems;
        GetItem -= OnGetItem;
        MoveItem -= OnMoveItem;
    }

    #region Custom Events
    private void OnAddItem(Item item, int stackSize) {

        if(Inventoy.Count < Capacity) {

            for (int i = 0; i < Capacity; i++) {
                if(!Inventoy.ContainsKey(i)) {
                    Inventoy.Add(i, new ItemSlot(item, stackSize));
                }
            }
        }
    }

    private void OnRemoveItems(int index, int amount) {
        ItemSlot slot;
        if (Inventoy.TryGetValue(index, out slot)) {
            int stack = slot.StackSize - amount;

            //TODO: send item to mouse interact
        }
    }

    private Item OnGetItem(int index) {
        ItemSlot slot;
        if (Inventoy.TryGetValue(index, out slot)) {
            return slot.Item;
        }
        return null;
    }

    private void OnMoveItem(int indexFrom, int indexTo) {

    }
    #endregion

    private void DropItemInWorld(Item item) {
        GameObject o = (GameObject)Instantiate(Resources.Load($"Prefabs/Items/{item.ID}"));
        o.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
