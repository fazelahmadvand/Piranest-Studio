using System.Collections;
using UnityEngine;

namespace Piranest
{
    public class TextureDownloader : MonoBehaviour
    {
        [SerializeField] private TextureSaveData saveData;
        [SerializeField] private VendorData vendorData;

        public IEnumerator Download()
        {
            foreach (var item in vendorData.Vendors)
            {
                if (saveData.HasTexture(item.Id)) continue;
                yield return API.API.DownloadTexture(item.ImageUrl, (tex) =>
                {
                    saveData.AddTexture(item.Id, tex);
                });
            }
            saveData.Save();

        }



    }
}
