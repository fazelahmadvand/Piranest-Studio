using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Piranest
{
    public abstract class SaveData<T> : ScriptableObject where T : new()
    {
        [field: SerializeField] public T Data { get; set; }

        public event Action<T> OnSaveChange;
        protected abstract string KeyName();

        public virtual void Init()
        {
            if (HasKey())
            {
                Load();
            }
            else
            {
                Data = new();
                Save();
            }
        }

        protected bool HasKey()
        {
            return PlayerPrefs.HasKey(KeyName());
        }

        public virtual void Save()
        {
            var json = JsonConvert.SerializeObject(Data);
            PlayerPrefs.SetString(KeyName(), json);
        }

        public void Load()
        {
            var json = PlayerPrefs.GetString(KeyName());
            Data = JsonConvert.DeserializeObject<T>(json);
            OnSaveChange?.Invoke(Data);
        }



    }



}
