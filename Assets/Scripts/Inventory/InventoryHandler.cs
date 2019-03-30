using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHandler : MonoBehaviour
{
    public static readonly int EquippedInventorySize = 7;
    
    public static event Action<int, ItemSlot> OnChangedSelectedSlot;
    public static event Action<List<ItemSlot>> UpdatedInventory;

    public static Action<Item, int> AddItem;
    public static Action<int, int> RemoveItems;
    public static Action<int, int> MoveItem;
    public static Action<int> UseItem;

    public int Capacity = 32;

    private List<ItemSlot> Inventory;

    private int _selectedIndex;
    private int SelectedIndex { get { return _selectedIndex; }
        set {
            _selectedIndex = Mathf.Clamp(value, 0, EquippedInventorySize - 1);

            OnChangedSelectedSlot?.Invoke(_selectedIndex, Inventory[_selectedIndex]);
            OnRemoveItems(_selectedIndex, 0);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Inventory = new List<ItemSlot>();

        OnAddItem((Item)Resources.Load("Scriptable Objects/IronNugget"), 1);
        OnAddItem((Item)Resources.Load("Scriptable Objects/IronNugget"), 2);
        OnAddItem((Item)Resources.Load("Scriptable Objects/IronNugget"), 3);
        OnAddItem((Item)Resources.Load("Scriptable Objects/IronNugget"), 4);
        OnAddItem((Item)Resources.Load("Scriptable Objects/IronNugget"), 5);

        SelectedIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectedIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectedIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectedIndex = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SelectedIndex = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SelectedIndex = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            SelectedIndex = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            SelectedIndex = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            SelectedIndex = 7;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            SelectedIndex = 8;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            SelectedIndex = 9;
        }

        if (Input.mouseScrollDelta.y < 0f) 
        {
            SelectedIndex--;
        }

        if (Input.mouseScrollDelta.y > 0f) {
            SelectedIndex++;
        }
    }

    private void OnEnable() {
        AddItem += OnAddItem;
        RemoveItems += OnRemoveItems;
        MoveItem += OnMoveItem;
        UseItem += OnUseItem;
    }

    private void OnDestroy() {
        AddItem -= OnAddItem;
        RemoveItems -= OnRemoveItems;
        MoveItem -= OnMoveItem;
        UseItem -= OnUseItem;
    }

    #region Custom Events
    private void OnAddItem(Item item, int stackSize) {

        // If inventory is not full
        if(Inventory.Count < Capacity) {

            // Find the first available slot, and add the item to it. 
            for (int i = 0; i < Capacity; i++) {
                if(Inventory[i] == null) {
                    ItemSlot itemSlot = new ItemSlot(item, stackSize);
                    Inventory.Add(itemSlot);

                    UpdatedInventory(Inventory);

                    break;
                }
            }
        }
    }

    private void OnRemoveItems(int index, int amount) {
        ItemSlot slot = Inventory[index];

        if(slot != null) {
            slot.StackSize -= amount;

            if (amount <= 0 || slot.StackSize <= 0) {
                slot = null;
            }
        }
    }

    private void OnMoveItem(int indexFrom, int indexTo) {

    }

    private void OnUseItem(int indexFrom) {

    }

    #endregion


    private void DropItemInWorld(Item item) {
        GameObject o = (GameObject)Instantiate(Resources.Load($"Prefabs/Items/{item.ID}"));
        o.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
