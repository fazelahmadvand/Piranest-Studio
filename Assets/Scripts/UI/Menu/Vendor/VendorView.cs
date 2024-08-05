using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class VendorView : View
    {
        [SerializeField] private VendorCardView vendorCard;
        [SerializeField] private Transform vendorHolder;


        private readonly List<VendorCardView> vendorCards = new();
        [SerializeField] private TextureSaveData textureData;
        [SerializeField] private VendorData vendorData;

        public override void InitView()
        {
            base.InitView();

            vendorData.OnLoadVendors += OnLoadVendors;
        }

        private void OnLoadVendors(List<Model.Vendor> vendors)
        {
            vendorHolder.DestroyChildren();
            foreach (Model.Vendor vendor in vendors)
            {
                var newCard = Instantiate(vendorCard, vendorHolder);
                newCard.UpdateCard(vendor, textureData.GetSprite(vendor.Id), () =>
                {
                    Debug.Log(vendor.Id);
                });
                vendorCards.Add(newCard);
            }



        }




    }
}
