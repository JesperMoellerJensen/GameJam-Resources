using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public static Action<Item> AddItem;
    public static Action<int> RemoveItem;
    public static Action<int> GetItem;
    public static Action<int, int> MoveItem;

    public Dictionary<int, Item> Inventoy;
    public int Capacity = 32;

    // Start is called before the first frame update
    void Start()
    {
        Inventoy = new Dictionary<int, Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        AddItem += OnAddItem;
        RemoveItem += OnRemoveItem;
        GetItem += OnGetItem;
        MoveItem += OnMoveItem;
    }

    private void OnDestroy() {
        AddItem -= OnAddItem;
        RemoveItem -= OnRemoveItem;
        GetItem -= OnGetItem;
        MoveItem -= OnMoveItem;
    }

    #region Custom Events
    private void OnAddItem(Item item) {

        if(Inventoy.Count < Capacity) {

            for (int i = 0; i < Capacity; i++) {
                if(!Inventoy.ContainsKey(i)) {
                    Inventoy.Add(i, item);
                }
            }
        }
    }

    private void OnRemoveItem(int index) {
        Item item = null;
        if (Inventoy.TryGetValue(index, out item)) {
            Inventoy.Remove(index);
            DropItemInWorld(item);
        }
    }

    private void OnGetItem(int index) {

    }

    private void OnMoveItem(int indexFrom, int indexTo) {

    }
    #endregion

    private void DropItemInWorld(Item item) {
        UnityEngine.GameObject o = (UnityEngine.GameObject)Instantiate(Resources.Load($"Prefabs/Items/{item.ID}"));
        o.transform.position = UnityEngine.GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
