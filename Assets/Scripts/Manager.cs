using Piranest.SaveSystem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    public class Manager : Singleton<Manager>
    {
        [SerializeField] private List<BaseServiceData> servicesData;
        [SerializeField] private TextureDownloader textureDownloader;
        [Header("Saved Data")]
        [SerializeField] private UserSaveData userSaveDataes;
        [SerializeField] private TextureSaveData textureSaveData;

        [SerializeField] private List<View> views;

        public event Action OnInitialized;

        private void Awake()
        {
            userSaveDataes.Init();
            textureSaveData.Init();
        }

        private async void Start()
        {
            LoadingHandler.Instance.Show();
            foreach (var view in views)
            {
                view.InitView();
            }

            await Task.Delay(1000);
            foreach (var data in servicesData)
            {
                await data.Init();
            }
            await Task.Delay(10);
            await textureDownloader.Download();
            await Task.Delay(500);
            OnInitialized?.Invoke();

        }






    }
}
