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


        public async Task Download()
        {
            var urls = new List<string>();
            urls.AddRange(itemData.Vendors.Select(v => v.ImageUrl));
            urls.AddRange(gameData.Games.Select(g => g.CoverImageUrl));
            urls.AddRange(gameData.Games.Select(g => g.DetailsImageUrl));
            urls.AddRange(gameData.GameChapters.Select(g => g.MediaUrl));
            urls.AddRange(gameData.GameChapterQuestions.Select(g => g.MediaUrl));
            urls.AddRange(gameData.GameChapterQuestions.Select(g => g.AnswerMediaUrl));


            foreach (var item in itemData.Vendors)
            {
                if (saveData.HasTexture(item.ImageUrl)) continue;
                var tex = await API.API.DownloadTexture(item.ImageUrl);
                if (tex != null)
                {
                    saveData.AddTexture(item.ImageUrl, tex);
                }
            }


            saveData.Save();

        }


    }
}
