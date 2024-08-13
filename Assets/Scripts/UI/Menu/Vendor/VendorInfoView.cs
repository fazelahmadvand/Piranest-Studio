using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Piranest.UI.Menu
{
    public class VendorInfoView : View
    {
        //[SerializeField] private Image img;
        [SerializeField] private Button locationBtn;


        [SerializeField] private VendorData vendorData;
        [SerializeField] private TextureSaveData textureSaveData;

        public void UpdateCard(int id)
        {

            var vendor = vendorData.GetVendor(id);
            if (vendor == null)
            {
                Debug.Log($"No Vendor Found:{id}");
                return;
            }
            Show();
            locationBtn.onClick.AddListener(() =>
            {
                Utility.OpenGoogleMap(vendor.LocationLat, vendor.LocationLong);
                Debug.Log("Clicked");
            });
            var sprite = textureSaveData.GetSprite(vendor.ImageUrl);
            //img.sprite = sprite;

        }



    }
}
