using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class VendorCouponCardView : View
    {

        [SerializeField] private Button btn;
        [SerializeField] private TMP_Text gemCountTxt;
        [SerializeField] private TMP_Text discountTxt;


        public void UpdateCard(string discount, int gem, Action OnClick)
        {
            Show();
            discountTxt.text = discount;
            gemCountTxt.text = gem.ToString();
            btn.AddEvent(OnClick);

        }


    }
}
