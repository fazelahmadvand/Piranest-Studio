using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    public class TextureDownloader : Singleton<TextureDownloader>
    {
        [SerializeField] private TextureSaveData saveData;
        [SerializeField] private ItemData vendorData;


        //private Dictionary<string, Texture2D> textures = new();

        public async Task Download()
        {
            foreach (var item in vendorData.Vendors)
            {
                if (saveData.HasTexture(item.ImageUrl)) continue;
                var tex = await API.API.DownloadTexture(item.ImageUrl);
                if (tex != null)
                {
                    //textures.Add(item.ImageUrl, tex);
                    saveData.AddTexture(item.ImageUrl, tex);
                }
            }
            saveData.Save();

        }

        //public Sprite GetSprite(string url)
        //{
        //    if (!textures.ContainsKey(url))
        //    {
        //        Debug.LogError($"Not Found: {url}");
        //        return null;
        //    }

        //    return Utility.GetSprite(textures[url]);

        //}

    }
}
