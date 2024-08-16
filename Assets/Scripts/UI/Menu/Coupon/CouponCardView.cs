using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class CouponCardView : View
    {

        [SerializeField] private Button btn;
        [SerializeField] private TMP_Text gemCountTxt;
        [SerializeField] private TMP_Text infoTxt;


        public void UpdateCard(string discount, int gem, Action OnClick)
        {
            Show();
            infoTxt.text = discount;
            gemCountTxt.text = gem.ToString();
            btn.AddEvent(OnClick);

        }


    }
}
