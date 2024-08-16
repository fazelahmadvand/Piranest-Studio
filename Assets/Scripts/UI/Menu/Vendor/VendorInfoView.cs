using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Piranest.UI.Menu
{
    public class VendorInfoView : View
    {
        [SerializeField] private Image img;
        [SerializeField] private Button locationBtn;

        [SerializeField] private Transform couponParent;
        [SerializeField] private VendorCouponCardView couponCard;

        [SerializeField] private VendorData vendorData;
        [SerializeField] private TextureSaveData textureSaveData;


        private List<VendorCouponCardView> cards = new();

        public void UpdateCard(int id)
        {
            var vendor = vendorData.GetVendor(id);
            if (vendor == null)
            {
                Debug.Log($"No Vendor Found:{id}");
                return;
            }
            Show();

            locationBtn.AddEvent(() =>
            {
                Utility.OpenGoogleMap(vendor.LocationLat, vendor.LocationLong);
            });

            var sprite = textureSaveData.GetSprite(vendor.ImageUrl);
            img.sprite = sprite;

            FakeCoupon();
        }


        private void FakeCoupon()
        {
            couponParent.DestroyChildren();
            for (int i = 0; i < 5; i++)
            {
                int start = 10 + (i * 10);
                int gem = start * 120;
                var card = Instantiate(couponCard, couponParent);
                card.UpdateCard($"{start}", gem, () =>
                {
                    Debug.Log("Coupon");
                });
                cards.Add(card);
            }
        }



    }
}
