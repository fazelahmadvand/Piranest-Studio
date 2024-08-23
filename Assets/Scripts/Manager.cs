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
            var loading = LoadingHandler.Instance;
            loading.Show();
            loading.UpdateText("Initialize View");
            foreach (var view in views)
            {
                view.InitView();
            }

            await Task.Delay(1000);
            loading.UpdateText("Getting Data From Server");
            foreach (var data in servicesData)
            {
                await data.Init();
            }

            await Task.Delay(10);
            await textureDownloader.Download((total, current) =>
            {
                loading.UpdateText($"Downloadng Images {total} / {current}");
            });

            loading.UpdateText($"");
            await Task.Delay(50);
            OnInitialized?.Invoke();
            GameManager.Instance.Init();

        }






    }
}
