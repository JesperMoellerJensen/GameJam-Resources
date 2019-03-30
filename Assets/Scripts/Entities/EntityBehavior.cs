using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EntityBehavior : MonoBehaviour {

    public MonoBehaviour Script;
    public List<Item> RequiredItems;

    private void Start() {
        Script.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.8f, 1, 0.6f);
    }


    private void OnEnable() {
        EventManager.AddListener("AddItemToEntity", OnInteract);
    }

    public object OnInteract(object eventArgs) {
        object[] data = (object[])eventArgs;
        GameObject entity = (GameObject)data[0];

        if (entity == transform.root.gameObject) {
            Item item = (Item)data[1];
            int amount = (int)data[2];

            Interact(item, amount)
        } 
    }

    public bool Interact(Item item, int amount) {
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
        if(Script.GetType() == typeof(SmelterBehavior)) {
            SmelterBehavior smelter = (SmelterBehavior)Script;
        }
    }
}


