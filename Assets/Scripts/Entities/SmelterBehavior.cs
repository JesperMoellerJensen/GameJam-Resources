using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterBehavior : MonoBehaviour
{
	public Item ItemToSmelt;
	public int ItemsInSmelter;

	[SerializeField]
	private bool isSmelting = false;
	private float currentSmeltingTime = 0;

	private Dictionary<ItemID, float> smeltTimeOfItems;
	

    // Start is called before the first frame update
    void Start()
    {
		ItemToSmelt = null;
		ItemsInSmelter = 0;

		smeltTimeOfItems = new Dictionary<ItemID, float>()
		{
			{ItemID.CopperOre, 5f},
			{ItemID.IronOre, 5f},
			{ItemID.GoldOre, 5f}
		};
    }

    // Update is called once per frame
    void Update()
    {
		if(ItemToSmelt == null)
		{
			return;
		}

		if(ItemsInSmelter > 0)
		{
			currentSmeltingTime += Time.deltaTime;
			if(currentSmeltingTime >= smeltTimeOfItems[ItemToSmelt.ID])
			{
				ItemsInSmelter--;
				currentSmeltingTime = 0;
			}
		}
	}

	/// <summary>
	/// eventArgs[0] == GameObject of the chosen smelter.
	/// eventArgs[1] == Item to smelt
	/// Returns true if item is smeltable
	/// </summary>
	/// <param name="eventArgs">object[]</param>
	private bool OnAddResouceToSmelter(object eventArgs)
	{
		object[] data = (object[])eventArgs;
		GameObject smelter = (GameObject)data[0];

		if(smelter == this.gameObject)
		{
			Item item = (Item)data[1];

			if (VerifyResource(item))
			{
				ItemToSmelt = item;
				ItemsInSmelter++;

				return true;
			}
		}

		return false;
	}


	public bool VerifyResource(Item item)
	{
		for (int i = 0; i < smeltTimeOfItems.Count; i++)
		{
			if(smeltTimeOfItems.ContainsKey(item.ID))
			{
				return true;
			}
		}

		return false;
	}
}
