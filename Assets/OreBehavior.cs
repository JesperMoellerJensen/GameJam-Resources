using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class OreBehavior : EntityBehavior
{
    public Item DroppedNugget;
    public int DroppedAmount;

    public override ItemSlot Interact(ItemSlot selectedItemSlot) {

        for (int i = 0; i < DroppedAmount; i++) {
            GameObject nugget = Instantiate(Resources.Load<GameObject>("Prefabs/Item"), transform.position, transform.rotation);
            nugget.GetComponent<ItemPickup>().item = Resources.Load<Item>($"Scriptable Objects/{DroppedNugget.ResourceType}Nugget");
            nugget.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        }

        Destroy(gameObject);

        return selectedItemSlot;
    }
}
