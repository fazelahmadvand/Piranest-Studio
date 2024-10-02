using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Piranest.SaveSystem
{
    [CreateAssetMenu(fileName = nameof(TimerSaveData), menuName = Utility.SCRIPTABLE_PATH + nameof(TimerSaveData))]
    public class TimerSaveData : SaveData<TimerSaveData.Timer>
    {
        private DateTime lastLogin;

        protected override string KeyName()
        {
            return "TimerSaveData";
        }

        public override void Init()
        {
            bool isAlreadyInited = HasKey();
            base.Init();
            lastLogin = DateTime.Parse(Data.lastLogin);
            if (isAlreadyInited)
            {
                foreach (var map in Data.times)
                {
                    map.Value -= LastRunAppTotalSecondDiff();
                }
                Save();
            }
        }

        public override void Save()
        {
            foreach (var time in Data.times)
            {
                if (time.Value < -1)
                    time.Value = -1;
            }
            base.Save();
        }

        public void AddTime(int gameId, int timer)
        {
            var map = new Map<int, int>(gameId, timer);
            Debug.Log($"Add Game Timer: {gameId} - {timer}");
            Data.times.Add(map);
            Save();
        }

        public bool HasGame(int gameId)
        {
            var map = Data.times.FirstOrDefault(d => d.Key == gameId);
            return map != null;
        }

        public int GetTime(int gameId)
        {
            var map = Data.times.FirstOrDefault(d => d.Key == gameId);
            return map == null ? -1 : map.Value;
        }

        private int LastRunAppTotalSecondDiff()
        {
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeDifference = currentTime - lastLogin;
            int elapsedSeconds = (int)timeDifference.TotalSeconds;
            return elapsedSeconds;
        }

        [Serializable]
        public class Timer
        {
            public string lastLogin;
            public List<Map<int, int>> times;

            public Timer()
            {
                lastLogin = DateTime.UtcNow.ToString();
                times = new();
            }
        }
    }
}
