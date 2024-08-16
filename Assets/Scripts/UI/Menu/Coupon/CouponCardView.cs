using Piranest.Model;
using TMPro;
using UnityEngine;
namespace Piranest.UI.Menu
{
    public class CouponCardView : View
    {

        [SerializeField] private TMP_Text codeTxt;
        [SerializeField] private TMP_Text discountTxt;



        public void UpdateCard(Coupon coupon)
        {
            codeTxt.text = coupon.Code;
            discountTxt.text = coupon.Amount.ToString();
        }

    }
}
