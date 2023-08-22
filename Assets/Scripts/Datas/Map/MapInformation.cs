using Assets.Scripts.Datas;
using Assets.Scripts.Enums;
using UnityEngine;

public class MapInformation : MonoBehaviour
{
	[Header("Data For Begin")]
	[SerializeField] private int id;
	[SerializeField] private bool unlocked = false;
	[SerializeField] private MapLevel level;
	[SerializeField] private bool levelUnlocked;

	//[Header("Data For Save")]
	//public MapData mapData;

	readonly string lockImageName = "MapLockImage";
	readonly string unlockImageName = "MapUnlockImage";

	public int Id { get => id; set => id = value; }
	public bool Unlocked { get => unlocked; set => unlocked = value; }
	public MapLevel Level { get => level; set => level = value; }
	public bool LevelUnlocked { get => levelUnlocked; set => levelUnlocked = value; }

	public void ShowMapStatus()
	{
		if (id <= GameDatas.LastMapIdOpen)
		{
			unlocked = true;
		}

		transform.Find(unlockImageName).gameObject.SetActive(unlocked);
		transform.Find(lockImageName).gameObject.SetActive(!unlocked);
	}

	public void ShowMapDetail()
	{
		if (!unlocked) return;

		GameDatas.MapId = id;
		GameDatas.MapLevel = level;
	}

	//public void DisplayMap(MapData data)
	//{
	//	mapData = data;

	//	transform.Find(unlockImageName).gameObject.SetActive(data.unlocked);
	//	transform.Find(lockImageName).gameObject.SetActive(!data.unlocked);
	//}

	//public void ShowMapDetail()
	//{
	//	if (!mapData.unlocked) return;

	//	GameDatas.MapId = mapData.id;
	//	GameDatas.MapLevelOpen = mapData.level.ToString();
	//}
}

//public class  DataMap
//{
//	public static int currentMapPlay = 0;

//	static List<List<int>> AllStageWaves = new List<List<int>>
//	{
//		new List < int > { 7, 1, 3 },//map 1
//		new List < int > { 2, 1, 3 },//map 2
//	};

//	public static List<int> GetallWaves(int stage)
//	{
//		return AllStageWaves[stage];
//	}

//	//mENNU
//	void ClickStartGame(int map)
//	{
//		DataMap.currentMapPlay = map;
//	}

//	//gMAE
//	void StartGame()
//	{
//		//Load wave
//		var waves = DataMap.GetallWaves(DataMap.currentMapPlay);

//		for (int i = 0; i < waves.Count; i++)
//		{
//			var go = Resources.Load<GameObject>($"Wave-{1}");
//			GameObject.Instantiate(go);//wave tren map
//		}

//	}
//}