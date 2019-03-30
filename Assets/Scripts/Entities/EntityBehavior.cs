using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour {

    public MonoBehaviour Script;
    public List<Item> RequiredItems;

    private void Start() {
        Script.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.8f, 1, 0.6f);
    }
}


