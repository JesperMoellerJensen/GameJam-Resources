using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
	Copper,
	Iron,
	Gold,
	Wood
}

public enum ItemID
{
	CopperOre = 0,
	CopperBar = 1,
	IronOre = 2,
	IronBar = 3,
	GoldOre = 4,
	GoldBar = 5,
	Wood = 6
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item", order = 1)]
public class Item : ScriptableObject
{
	public ItemID ID;
	public string DisplayName;
	public string Description;
	public ResourceType ResourceType;
}

