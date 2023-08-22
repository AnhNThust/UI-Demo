using Assets.Scripts.Datas;
using Assets.Scripts.Enums;
using Assets.Scripts.Statics;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDatas
{
	#region Constant

	private const string ENERGY_KEY = "Energy";
	private const string GOLD_KEY = "Gold";
	private const string GEM_KEY = "Gem";

	private const string PLANE_USED_ID_KEY = "PlaneUsedId";
	private const string PLANE_SELECTED_ID_KEY = "PlaneSelectedId";
	private const string NUMBER_PLANE_UNLOCKED_KEY = "NumberPlaneUnlocked";

	private const string MAP_ID_KEY = "MapId";
	private const string MAP_LEVEL_KEY = "MapLevel";

	#endregion Constant

	#region Currency

	public static int Gold
	{
		get
		{
			return PlayerPrefs.GetInt(GOLD_KEY, 0);
		}

		set
		{
			PlayerPrefs.SetInt(GOLD_KEY, value);
			EventDispatcher.PostEvent(EventID.GoldChanged, value);
		}
	}

	public static int Gem
	{
		get
		{
			return PlayerPrefs.GetInt(GEM_KEY, 0);
		}

		set
		{
			PlayerPrefs.SetInt(GEM_KEY, value);
			EventDispatcher.PostEvent(EventID.GemChanged, value);
		}
	}

	public static int Energy
	{
		get
		{
			return PlayerPrefs.GetInt(ENERGY_KEY, 30);
		}

		set
		{
			PlayerPrefs.SetInt(ENERGY_KEY, value);
			EventDispatcher.PostEvent(EventID.EnergyChanged, value);
		}
	}

	#endregion Currency

	#region Plane

	public static int PlaneUsedId
	{
		get
		{
			return PlayerPrefs.GetInt(PLANE_USED_ID_KEY, 1);
		}

		set
		{
			PlayerPrefs.SetInt(PLANE_USED_ID_KEY, value);
			EventDispatcher.PostEvent(EventID.PlaneUsedIdChanged, value);
		}
	}

	public static int PlaneSelectId
	{
		get => PlayerPrefs.GetInt(PLANE_SELECTED_ID_KEY, 1);

		set
		{
			PlayerPrefs.SetInt(PLANE_USED_ID_KEY, value);
			EventDispatcher.PostEvent(EventID.PlaneSelectedChanged, value);
		}
	}

	public static int NumberPlaneUnlocked
	{
		get
		{
			return PlayerPrefs.GetInt(NUMBER_PLANE_UNLOCKED_KEY, 1);
		}

		set
		{
			PlayerPrefs.SetInt(NUMBER_PLANE_UNLOCKED_KEY, value);
			EventDispatcher.PostEvent(EventID.NumberPlaneUnlockedChanged, value);
		}
	}

	public static void SavePlaneData(PlaneData data)
	{
		string keyPlane = $"{PlaneKey.PREFIX_KEY}{data.id}_";
		PlayerPrefs.SetInt($"{keyPlane}{PlaneKey.UNLOCKED_KEY}", data.unlocked ? 1 : 0);
		PlayerPrefs.SetInt($"{keyPlane}{PlaneKey.LEVEL_KEY}", data.level);
		PlayerPrefs.SetInt($"{keyPlane}{PlaneKey.PRICE_UNLOCK_KEY}", data.priceUnlock);
		PlayerPrefs.SetInt($"{keyPlane}{PlaneKey.PRICE_UP_GOLD_KEY}", data.priceUpgradeGold);
		PlayerPrefs.SetInt($"{keyPlane}{PlaneKey.PRICE_UP_GEM_KEY}", data.priceUpgradeGem);
	}

	//public static PlaneDatas GetPlaneData(int planeId)
	//{
	//	string keyPlane = $"Plane{planeId}";

	//	// Tao mot object PlaneDatas moi
	//	PlaneDatas datas = new PlaneDatas() {
	//		id = planeId,
	//		level = PlayerPrefs.GetInt($"{keyPlane}-lv", ),
	//		unlocked = PlayerPrefs.GetInt($"{keyPlane}-unlock", 0) == 1
	//	};

	//	// Tra ve 1 object PlaneDatas
	//	return datas;
	//}

	public static PlaneData GetPlaneData(PlaneInformation planeInfo)
	{
		return new PlaneData(planeInfo);
	}

	#endregion Plane

	#region Map

	public static int CurrentStage
	{
		get => PlayerPrefs.GetInt($"{StageKey.PREFIX_KEY}{StageKey.CURRENT_KEY}", 1);
		set
		{
			PlayerPrefs.SetInt($"{StageKey.PREFIX_KEY}{StageKey.CURRENT_KEY}", value);
		}
	}

	// Map 10, 20, ... Sẽ có Boss
	// Map 9 có special enemy bắn toả tròn
	public static List<List<int>> Stage_1 = new List<List<int>>
	{
		new List<int> { 1, 2, 3, 4 }, // Map 1
		new List<int> { 2, 5, 7, 8 }, // Map 2
		new List<int> { 3, 11, 12, 6 }, // Map 3
		new List<int> { 1, 4, 3, 9 }, // Map 4
		new List<int> { 1, 3, 4, 10 }, // Map 5
		new List<int> { 2, 5, 3, 1 }, // Map 6
		new List<int> { 4, 7, 2, 8 }, // Map 7
		new List<int> { 2, 5, 11, 6 }, // Map 8
		new List<int> { 2, 13, 4, 9 }, // Map 9
		new List<int> { 2, 4, 6, 8 }, // Map 10
		new List<int> { 1, 3, 5, 7 }, // Map 11
		new List<int> { 1, 2, 5, 6 }, // Map 12
		new List<int> { 4, 2, 8, 9 }, // Map 13
		new List<int> { 3, 11, 1, 12 }, // Map 14
		new List<int> { 2, 7, 6, 10 }, // Map 15
		new List<int> { 13, 1, 4, 9 }, // Map 16
		new List<int> { 2, 3, 7, 8 }, // Map 17
		new List<int> { 1, 4, 9, 12 }, // Map 18
		new List<int> { 3, 6, 2, 13 }, // Map 19
		new List<int> { 5, 8, 9, 20 }, // Map 20
	};

	public static List<int> GetAllWaveOfMap(int mapId)
	{
		return Stage_1[mapId - 1];
	}

	public static int LastMapIdOpen
	{
		get => PlayerPrefs.GetInt($"{MapKey.LAST_MAP_ID_OPEN}", 1);
		set
		{
			PlayerPrefs.SetInt($"{MapKey.LAST_MAP_ID_OPEN}", value);
		}
	}

	public static int MapId { get; set; }

	public static MapLevel LastMapLevelOpen
	{
		get
		{
			string level = PlayerPrefs.GetString($"{MapKey.LAST_MAP_LEVEL_OPEN}", Enum.GetName(typeof(MapLevel), MapLevel.Normal));
			return (MapLevel)Enum.Parse(typeof(MapLevel), level);
		}

		set
		{
			PlayerPrefs.SetString($"{MapKey.LAST_MAP_LEVEL_OPEN}", Enum.GetName(typeof(MapLevel), value));
		}
	}

	public static MapLevel MapLevel { get; set; }

	#endregion Map

	#region Player
	public static int PlayerBulletLevel { get; set; }
	#endregion
}