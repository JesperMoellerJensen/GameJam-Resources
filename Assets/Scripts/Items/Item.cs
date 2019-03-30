using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
	Copper,
	Iron,
	Gold,
	Wood,
    Carbon
}

public enum ItemID
{
	CopperNugget = 0,
	CopperBar = 1,
    IronNugget = 2,
	IronBar = 3,
    GoldNugget = 4,
	GoldBar = 5,
	Wood = 6,
    CarbonCrystal = 7
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item", order = 1)]
public class Item : ScriptableObject
{
	public ItemID ID;
	public string DisplayName;
	public string Description;
	public ResourceType ResourceType;
}

