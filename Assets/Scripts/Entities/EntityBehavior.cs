using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehavior : MonoBehaviour {

    public abstract ItemSlot Interact(ItemSlot selectedItemSlot);
}


