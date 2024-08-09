using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class VendorView : View
    {
        [SerializeField] private VendorCardView vendorCard;
        [SerializeField] private Transform vendorHolder;

        [SerializeField] private TextureSaveData textureData;
        [SerializeField] private VendorData vendorData;

        private readonly List<VendorCardView> vendorCards = new();

        public override void InitView()
        {
            base.InitView();

            Manager.Instance.OnInitialized += OnInitialized;
        }

        private void OnDestroy()
        {
            if (Manager.Instance)
                Manager.Instance.OnInitialized -= OnInitialized;
        }

        private void OnInitialized()
        {
            OnLoadVendors(vendorData.Vendors);
        }

        private void OnLoadVendors(List<Model.Vendor> vendors)
        {
            vendorHolder.DestroyChildren();
            foreach (Model.Vendor vendor in vendors)
            {
                var newCard = Instantiate(vendorCard, vendorHolder);
                var sprite = TextureDownloader.Instance.GetTexture(vendor.ImageUrl);
                newCard.UpdateCard(vendor, sprite, () =>
                {
                    Debug.Log(vendor.Id);
                });
                vendorCards.Add(newCard);
            }



        }




    }
}
