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
        [SerializeField] private VendorCouponPopUpView popUp;
        [SerializeField] private VendorCouponUnlockView unlockView;

        [Space]
        [SerializeField] private AuthData authData;

        private List<VendorCouponCardView> cards = new();

        public override void InitView()
        {
            base.InitView();
            popUp.InitView();
            unlockView.InitView();
            authData.OnCouponCreate += OnCouponCreate;
        }

        private void OnDestroy()
        {
            authData.OnCouponCreate -= OnCouponCreate;
        }

        public override void Hide()
        {
            base.Hide();
            popUp.Hide();
            unlockView.Hide();
        }

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

        private void OnCouponCreate(string code)
        {
            unlockView.UpdateCard(code);
            popUp.Hide();
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
                    if (authData.CanBuy(coupon.PriceAmount))
                    {
                        popUp.UpdateCard(coupon.DiscountPercentage, coupon.PriceAmount, async () =>
                        {
                            await authData.CreateCoupon(vendorId, -coupon.PriceAmount);
                        });
                    }
                    else
                    {
                        PopUpManager.Instance.Show("Not enough gem", null);
                    }

                });
                cards.Add(card);
            }

        }



    }
}
