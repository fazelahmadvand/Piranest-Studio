using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    public class TextureDownloader : MonoBehaviour
    {
        [SerializeField] private TextureSaveData saveData;
        [SerializeField] private VendorData vendorData;

        public async Task Download()
        {
            foreach (var item in vendorData.Vendors)
            {
                if (saveData.HasTexture(item.Id)) continue;
                var tex = await API.API.DownloadTexture(item.ImageUrl);
                if (tex != null)
                    saveData.AddTexture(item.Id, tex);
            }
            saveData.Save();

        }



    }
}
