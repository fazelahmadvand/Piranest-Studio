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


        public async Task Download(Action<int, int> OnDownload)
        {
            var urls = new List<string>();
            urls.AddRange(itemData.Vendors.Select(v => v.ImageUrl));
            urls.AddRange(gameData.Games.Select(g => g.CoverImageUrl));
            urls.AddRange(gameData.Games.Select(g => g.DetailsImageUrl));
            urls.AddRange(gameData.GameChapters.Select(g => g.MediaUrl));
            urls.AddRange(gameData.GameChapterQuestions.Select(g => g.MediaUrl));
            urls.AddRange(gameData.GameChapterQuestions.Select(g => g.AnswerMediaUrl));

            int index = 1;
            foreach (var url in urls)
            {
                OnDownload?.Invoke(urls.Count, index);
                index++;
                if (saveData.HasTexture(url)) continue;
                var tex = await API.API.DownloadTexture(url);
                if (tex != null)
                {
                    saveData.AddTexture(url, tex);
                }
            }


            saveData.Save();

        }


    }
}
