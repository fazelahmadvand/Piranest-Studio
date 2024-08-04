using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(SaveData), menuName = Utility.SCRIPTABLE_PATH + nameof(SaveData))]
    public class SaveData : ScriptableObject
    {
        [field: SerializeField] public SaveInfo SaveInfo { get; set; }


        public event Action<SaveInfo> OnSaveChange;
        private const string SAVE_KEY = "SaveData";

        public void Init()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY))
            {
                Load();
            }
            else
            {
                SaveInfo = new SaveInfo();
                Save();
            }
        }




        public void Save()
        {
            var json = JsonConvert.SerializeObject(SaveInfo);
            PlayerPrefs.SetString(SAVE_KEY, json);
        }

        public void Load()
        {
            var json = PlayerPrefs.GetString(SAVE_KEY);
            SaveInfo = JsonConvert.DeserializeObject<SaveInfo>(json);
            OnSaveChange?.Invoke(SaveInfo);
        }

    }


    [System.Serializable]
    public struct SaveInfo
    {
        public string email;
        public string password;
    }
}
