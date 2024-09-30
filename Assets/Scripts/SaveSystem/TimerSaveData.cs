using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Piranest.SaveSystem
{
    [CreateAssetMenu(fileName = nameof(TimerSaveData), menuName = Utility.SCRIPTABLE_PATH + nameof(TimerSaveData))]
    public class TimerSaveData : SaveData<List<Map<int, int>>>
    {
        protected override string KeyName()
        {
            return "TimerSaveData";
        }

        public void AddTime(int gameId, int timer)
        {
            var map = new Map<int, int>(gameId, timer);
            Data.Add(map);
            Save();
        }

        public int GetTime(int gameId)
        {
            var map = Data.FirstOrDefault(d => d.Key == gameId);
            return map == null ? -1 : map.Value;
        }


    }
}
