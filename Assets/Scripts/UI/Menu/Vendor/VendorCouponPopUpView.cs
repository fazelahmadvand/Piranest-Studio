using Piranest.UI.Menu;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class VendorCouponPopUpView : View
    {

        [SerializeField] private Button buyBtn, cancelBtn;
        [SerializeField] private VendorCouponCardView couponCard;


        public override void InitView()
        {
            base.InitView();
            cancelBtn.SetEvent(Hide);
        }

        public void UpdateCard(int discount, int gemCost, Action OnBuyClick)
        {
            Show();
            couponCard.UpdateCard(discount.ToString(), gemCost, null);
            buyBtn.SetEvent(OnBuyClick);
        }
    }
}
