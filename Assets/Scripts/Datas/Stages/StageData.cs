using System;
using Assets.Scripts.Statics;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class StageData
    {
        public int id;
        public bool unlocked;

        public StageData(StageInformation stageInfo)
        {
            string KeyMap = $"{StageKey.PREFIX_KEY}{stageInfo.Id}_";

            id = stageInfo.Id;
            int unlockedInt = PlayerPrefs.GetInt($"{KeyMap}{StageKey.UNLOCKED_KEY}", 0);
            unlocked = unlockedInt == 1;
        }
    }
}