using Piranest.SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest
{
    public class TimerHandler : MonoBehaviour
    {
        [SerializeField] private TimerSaveData timerSaveData;
        public static int Timer { get; private set; }

        private int currentGameId;
        public static event Action TimeEnd;
        public static event Action TimeUpdate;

        public static bool HasTime => Timer >= 0;


        private void Start()
        {
            StartCoroutine(CalculateTime());
        }

        public void Init(int gameId)
        {
            if (currentGameId == gameId) return;
            currentGameId = gameId;
            Timer = timerSaveData.GetTime(gameId);
        }

        private IEnumerator CalculateTime()
        {
            while (true)
            {
                timerSaveData.Data.lastLogin = DateTime.UtcNow.ToString();
                foreach (var map in timerSaveData.Data.times)
                {
                    map.Value--;
                    if (map.Key == currentGameId)
                    {
                        Timer = map.Value;
                        if (Timer < 0)
                        {
                            TimeEnd?.Invoke();
                        }
                        else
                        {
                            TimeUpdate?.Invoke();
                        }
                    }
                }
                timerSaveData.Save();
                yield return new WaitForSeconds(1);

            }
        }


    }
}
