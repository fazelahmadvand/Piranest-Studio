using Piranest.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class VendorCardView : ButtonView
    {
        [SerializeField] private Button locationBtn;
        [SerializeField] private TMP_Text coopanTxt;
        [SerializeField] private TextureSaveData textureData;

        public void UpdateCard(Vendor vendor, Action OnClick)
        {
            Show();
            var sprite = textureData.GetSprite(vendor.ImageUrl);
            img.sprite = sprite;
            coopanTxt.text = $"{vendor.MaxCoupon}";
            UpdateButton(vendor.Name, OnClick);

        }



    }
}
