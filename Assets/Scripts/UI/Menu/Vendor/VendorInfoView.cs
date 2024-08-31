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

        [SerializeField] private ItemData itemData;
        [SerializeField] private TextureSaveData textureSaveData;


        private List<VendorCouponCardView> cards = new();

        public void UpdateCard(int id)
        {
            var vendor = itemData.GetVendor(id);
            if (vendor == null)
            {
                Debug.Log($"No Vendor Found:{id}");
                return;
            }
            Show();

            locationBtn.SetEvent(() =>
            {
                Utility.OpenGoogleMap(vendor.LocationLat, vendor.LocationLong);
            });

            var sprite = textureSaveData.GetSprite(vendor.ImageUrl);
            img.sprite = sprite;

            CreateCoupons(vendor.Id);
        }


        private void CreateCoupons(int vendorId)
        {
            couponParent.DestroyChildren();
            var coupons = itemData.GetVendorCoupon(vendorId);

            foreach (var coupon in coupons)
            {
                var card = Instantiate(couponCard, couponParent);
                card.UpdateCard($"{coupon.DiscountPercentage}", coupon.PriceAmount, () =>
                {
                    Debug.Log("Coupon");
                });
                cards.Add(card);
            }

        }



    }
}
