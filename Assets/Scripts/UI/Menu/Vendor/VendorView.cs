using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class VendorView : View
    {
        [SerializeField] private View authView;
        [SerializeField] private VendorCardView vendorCard;
        [SerializeField] private Transform vendorHolder;

        [SerializeField] private VendorInfoView vendorInfo;
        [SerializeField] private HeaderView headerView;

        [SerializeField] private TextureSaveData textureData;
        [SerializeField] private ItemData itemData;

        private readonly List<VendorCardView> vendorCards = new();

        public override void InitView()
        {
            base.InitView();
            vendorInfo.InitView();
            Manager.OnInitialized += OnInitialized;
        }

        public override void Show()
        {
            authView.Hide();
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
            Manager.OnInitialized -= OnInitialized;
        }

        private void OnInitialized()
        {
            OnLoadVendors(itemData.Vendors);
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
