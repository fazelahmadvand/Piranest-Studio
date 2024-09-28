using Piranest.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class CouponView : View
    {
        [SerializeField] private CouponCardView cardView;
        [SerializeField] private Transform couponHolder;


        [SerializeField] private AuthData authData;

        private List<CouponCardView> cards = new();

        public override void InitView()
        {
            base.InitView();
            CreateCoupons(authData.Coupons);
            authData.OnCouponsChange += CreateCoupons;
        }

        private void OnDestroy()
        {
            authData.OnCouponsChange -= CreateCoupons;
        }

        private void CreateCoupons(List<Coupon> coupons)
        {
            couponHolder.DestroyChildren();
            cards.Clear();
            foreach (Coupon coupon in coupons)
            {
                var card = Instantiate(cardView, couponHolder);
                card.UpdateCard(coupon);
                cards.Add(card);
            }
        }



    }
}
