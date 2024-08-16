using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class VendorView : View
    {
        [SerializeField] private VendorCardView vendorCard;
        [SerializeField] private Transform vendorHolder;

        [SerializeField] private VendorInfoView vendorInfo;
        [SerializeField] private HeaderView headerView;

        [SerializeField] private TextureSaveData textureData;
        [SerializeField] private VendorData vendorData;

        private readonly List<VendorCardView> vendorCards = new();

        public override void InitView()
        {
            base.InitView();
            vendorInfo.InitView();
            Manager.Instance.OnInitialized += OnInitialized;
        }

        public override void Show()
        {
            base.Show();
            headerView.UpdatePage("Vendors");
        }

        public override void Hide()
        {
            base.Hide();
            vendorInfo.Hide();
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
                var sprite = textureData.GetSprite(vendor.ImageUrl);
                newCard.UpdateCard(vendor, sprite, () =>
                {
                    Hide();
                    vendorInfo.UpdateCard(vendor.Id);
                    headerView.UpdateHeader(vendor.Name, () =>
                    {
                        Show();
                        vendorInfo.Hide();
                        headerView.HandleBackButton(false);
                    });
                });
                vendorCards.Add(newCard);
            }



        }




    }
}
