using Piranest.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class VendorCardView : ButtonView
    {
        [SerializeField] private Image img;
        [SerializeField] private Button locationBtn;
        [SerializeField] private TMP_Text coopanTxt;
        public void UpdateCard(Vendor vendor, Sprite sprite, Action OnClick)
        {
            Show();
            img.sprite = sprite;
            coopanTxt.text = $"{vendor.MaxCoupon}";
            UpdateButton(vendor.Name, OnClick);

        }



    }
}
