using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class VendorView : View
    {
        [SerializeField] private Button loadArScene;
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
            loadArScene.SetEvent(() =>
            {
                SceneLoader.LoadScene(SceneName.ARMultipleObjects_Puzzles);
            });
            vendorInfo.InitView();
            OnInitialized();
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
                newCard.UpdateCard(vendor, () =>
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
