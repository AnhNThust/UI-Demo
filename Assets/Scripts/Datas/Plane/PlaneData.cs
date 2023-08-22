using Assets.Scripts.Statics;
using System;
using UnityEngine;

[Serializable]
public class PlaneData
{
	public int id;
	public bool unlocked;
	public int level;
	public int priceUnlock; //  Use Gem
	public int priceUpgradeGold;
	public int priceUpgradeGem;

	public PlaneData()
	{
	}

	public PlaneData(PlaneInformation planeInfo)
	{
		string keyPlane = $"{PlaneKey.PREFIX_KEY}{planeInfo.Id}_";

		id = planeInfo.Id;
		unlocked = PlayerPrefs.GetInt($"{keyPlane}{PlaneKey.UNLOCKED_KEY}", planeInfo.Unlocked ? 1 : 0) == 1;
		level = PlayerPrefs.GetInt($"{keyPlane}{PlaneKey.LEVEL_KEY}", planeInfo.Level);
		priceUnlock = PlayerPrefs.GetInt($"{keyPlane}{PlaneKey.PRICE_UNLOCK_KEY}", planeInfo.PriceUnlock);
		priceUpgradeGold = PlayerPrefs.GetInt($"{keyPlane}{PlaneKey.PRICE_UP_GOLD_KEY}", planeInfo.PriceUpgradeGold);
		priceUpgradeGem = PlayerPrefs.GetInt($"{keyPlane}{PlaneKey.PRICE_UP_GEM_KEY}", planeInfo.PriceUpgradeGem);
	}

	public string GetName()
	{
		return $"Hero_{id}";
	}
}