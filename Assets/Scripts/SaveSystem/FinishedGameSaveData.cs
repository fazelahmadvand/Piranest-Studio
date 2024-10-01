using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Piranest.SaveSystem
{
    [CreateAssetMenu(fileName = nameof(FinishedGameSaveData), menuName = Utility.SCRIPTABLE_PATH + nameof(FinishedGameSaveData))]
    public class FinishedGameSaveData : SaveData<List<Map<int, bool>>>
    {
        protected override string KeyName()
        {
            return nameof(FinishedGameSaveData);
        }


        public void GameFinished(int gameId)
        {
            Data.Add(new Map<int, bool>(gameId, true));
            Debug.Log("Add Game Finished: " + gameId);
            Save();
        }

        public bool HasFinishedGame(int gameId)
        {
            var game = Data.FirstOrDefault(d => d.Key == gameId);
            return game != null;
        }

    }
}
