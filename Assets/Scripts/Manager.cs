using Piranest.SaveSystem;
using Piranest.UI;
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
        [SerializeField] private TimerSaveData timerSaveData;
        [SerializeField] private FinishedGameSaveData finishedGameSaveData;

        private void Awake()
        {
            Utility.FixFrameRate();
            userSaveDataes.Init();
            textureSaveData.Init();
            timerSaveData.Init();
            finishedGameSaveData.Init();
        }

        private async void Start()
        {
            var popUp = PopUpManager.Instance;
            popUp.Hide();
            var loading = LoadingHandler.Instance;
            loading.Show();
            loading.UpdateText("Initialize View");

            await Task.Delay(1000);
            loading.UpdateText("Getting Data From Server");
            await GetServerData();

            await Task.Delay(100);
            await textureDownloader.Download((total, success, fail) =>
            {
                string text = $"Downloading images {success} / {total} (Success: {success}, Failed: {fail})";
                loading.UpdateText(text);
            });

            loading.UpdateText($"Ready!");
            await Task.Delay(500);
            SceneLoader.LoadScene(SceneName.MainMenu);
        }

        private async Task GetServerData()
        {
            foreach (var data in servicesData)
            {
                Debug.Log(data.name);
                await data.Init((ex) =>
                {
                    PopUpManager.Instance.Show("Get Data Faield", async () =>
                    {
                        await GetServerData();
                    }, "Try Again");
                });

            }
        }



    }
}
