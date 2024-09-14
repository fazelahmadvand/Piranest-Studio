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

        public static event Action OnInitialized;

        private void Awake()
        {
            Utility.FixFrameRate();
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
            await GetServerData();

            await Task.Delay(100);
            await textureDownloader.Download((total, success, fail) =>
            {
                string text = $"Downloading images {success} / {total} (Success: {success}, Failed: {fail})";
                loading.UpdateText(text);
            });

            loading.UpdateText($"");
            await Task.Delay(500);
            if (userSaveDataes.HasUser())
                loading.Hide();
            OnInitialized?.Invoke();
            Debug.Log("Manager OnInitialized");

        }

        private async Task GetServerData()
        {
            foreach (var data in servicesData)
            {
                await data.Init();
                Debug.Log($"Init Service: {data.name}");
            }
        }



    }
}
