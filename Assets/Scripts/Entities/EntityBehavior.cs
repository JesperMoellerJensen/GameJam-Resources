using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehavior : MonoBehaviour {

    public abstract bool Interact(Item item, int amount);
}


