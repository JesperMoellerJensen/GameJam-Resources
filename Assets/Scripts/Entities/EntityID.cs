using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum EntityType {
    Machine,
    Ore
}

public enum EntityID {
    Smelter,
    CopperOre
}

[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Entity", order = 1)]
public class Entity : ScriptableObject {
    public EntityType Type;
    public EntityID ID;
    public string DisplayName;
    public string Description;

    
}