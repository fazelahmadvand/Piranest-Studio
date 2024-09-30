using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    public class TextureDownloader : Singleton<TextureDownloader>
    {
        [SerializeField] private TextureSaveData saveData;
        [SerializeField] private ItemData itemData;
        [SerializeField] private GameData gameData;

        public async Task Download(Action<int, int, int> OnDownload)
        {
            var urls = new List<string>();
            urls.AddRange(itemData.Vendors.Select(v => v.ImageUrl));
            urls.AddRange(gameData.Games.Select(g => g.CoverImageUrl));
            urls.AddRange(gameData.Games.Select(g => g.DetailsImageUrl));
            urls.AddRange(gameData.GameChapters.Select(g => g.MediaUrl));
            urls.AddRange(gameData.GameChapterQuestions.Select(g => g.MediaUrl));
            urls.AddRange(gameData.GameChapterQuestions.Select(g => g.AnswerMediaUrl));

            int successCount = 1;
            int failedCount = 0;
            foreach (var url in urls)
            {
                if (saveData.HasTexture(url))
                {
                    successCount++;
                }
                else
                {
                    var tex = await HTTP.API.DownloadTexture(url);
                    if (tex != null)
                    {
                        saveData.AddTexture(url, tex);
                        successCount++;
                    }
                    else
                    {
                        failedCount++;
                    }
                }

                OnDownload?.Invoke(urls.Count, successCount, failedCount);

            }


            saveData.Save();

        }


    }
}
