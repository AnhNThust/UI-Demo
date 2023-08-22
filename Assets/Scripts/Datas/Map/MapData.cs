using Assets.Scripts.Enums;
using Assets.Scripts.Statics;
using System;
using UnityEngine;

namespace Assets.Scripts.Datas
{
	[Serializable]
	public class MapData
	{
		public int id;
		public bool unlocked;
		public MapLevel level;
		public bool levelUnlocked;

		//public MapData(MapInformation mapInfomation)
		//{
		//string keyMap = $"{MapKey.PREFIX_KEY}{mapInfomation.Id}_";

		//id = mapInfomation.Id;
		//unlocked = PlayerPrefs.GetInt($"{keyMap}{MapKey.UNLOCKED_KEY}", mapInfomation.Unlocked ? 1 : 0) == 1;

		//string mapLevelString = PlayerPrefs.GetString($"{keyMap}{MapKey.LEVEL_KEY}{mapInfomation.Level}", 
		//	mapInfomation.Level.ToString());
		//level = (MapLevel)Enum.Parse(typeof(MapLevel), mapLevelString);

		//levelUnlocked = PlayerPrefs.GetInt($"{keyMap}{MapKey.LEVEL_UNLOCKED_KEY}", 
		//	mapInfomation.LevelUnlocked ? 1 : 0) == 1;
		//}
	}
}
