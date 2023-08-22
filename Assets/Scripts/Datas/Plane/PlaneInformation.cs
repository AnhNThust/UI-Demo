using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class PlaneDatas
{
	public int id;
	public int level;
	public bool unlocked;
	readonly string KeyPlanePrefix = $"Plane_";

	public PlaneDatas(PlaneInformation planeInformation)
	{
		string KeyPlane = $"{KeyPlanePrefix}{planeInformation.Id}";

		id = planeInformation.Id;
		level = PlayerPrefs.GetInt($"{KeyPlane}_Level", planeInformation.Level);
		unlocked = PlayerPrefs.GetInt($"{KeyPlane}_Unlocked", planeInformation.Unlocked ? 1 : 0) == 1;
	}

	public string GetName()
	{
		return "Name" + id;
	}
}


public class PlaneInformation : MonoBehaviour
{
	public TextMeshProUGUI levelText;
	public GameObject lockImage;
	public GameObject selectImage;
	public GameObject InUseImage;
	public void DisplayPlane(PlaneData data)
	{
		planeData = data;

		levelText.text = data.level.ToString();
		lockImage.SetActive(!data.unlocked);
	}

	public void SetStateSelectImage(bool isSelect)
	{
		selectImage.SetActive(isSelect);
	}

	public void UnlockPlane()
	{
		lockImage.SetActive(!planeData.unlocked);
	}

	public void SetStateInUseImage(bool isUsed)
	{
		InUseImage.SetActive(isUsed);
	}

	public void UpdateLevel()
	{
		levelText.text = planeData.level.ToString();
	}

	[Header("Data For Begin")]
	[SerializeField] private int id;
	[SerializeField] private bool unlocked;
	[SerializeField] private int level;
	[SerializeField] private int priceUnlock; //  Use Gem
	[SerializeField] private int priceUpgradeGold;
	[SerializeField] private int priceUpgradeGem;
	[SerializeField] private string keyData;

	[Header("Data For Save")]
	//public PlaneDatas planeData;
	public PlaneData planeData;

	public int Id { get => id; set => id = value; }
	public bool Unlocked { get => unlocked; set => unlocked = value; }
	public int Level { get => level; set => level = value; }
	public int PriceUnlock { get => priceUnlock; set => priceUnlock = value; }
	public int PriceUpgradeGold { get => priceUpgradeGold; set => priceUpgradeGold = value; }
	public int PriceUpgradeGem { get => priceUpgradeGem; set => priceUpgradeGem = value; }
	public string KeyData { get => keyData; set => keyData = value; }
}
