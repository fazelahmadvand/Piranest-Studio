using Piranest.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class VendorCardView : ButtonView
    {
        [SerializeField] private RawImage img;
        [SerializeField] private TMP_Text coopanTxt;

        public void UpdateCard(Vendor vendor, Texture2D sprite, Action OnClick)
        {
            Show();
            img.texture = sprite;
            coopanTxt.text = $"{vendor.MaxCoupon}%";
            UpdateButton(vendor.Name, OnClick);

        }



    }
}
