using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class ProfileView : View
    {
        [SerializeField] private CouponView couponView;
        [SerializeField] private Button editProfileButton, couponBtn;
        [SerializeField] private EditProfileView editProfileView;
        [SerializeField] private HeaderView headerView;

        [Space]
        [SerializeField] private TMP_Text gemTxt;
        [SerializeField] private TMP_Text rankTxt;

        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            base.InitView();
            rankTxt.text = "1";
            editProfileView.InitView();
            editProfileButton.AddEvent(() =>
            {
                Hide();
                editProfileView.Show();
                headerView.UpdateHeader("Edit Profile", () =>
                {
                    editProfileView.Hide();
                    Show();
                    headerView.HandleBackButton(false);

                });
            });

            couponBtn.AddEvent(() =>
            {
                Hide();
                couponView.Show();
                headerView.UpdateHeader("My Coupons", () =>
                {
                    couponView.Hide();
                    Show();
                    headerView.HandleBackButton(false);

                });
            });

            authData.OnAccountChange += OnAccountChange;
        }

        private void OnDestroy()
        {
            authData.OnAccountChange -= OnAccountChange;
        }

        private void OnAccountChange(Model.Account account)
        {
            gemTxt.text = account.Remaining.ToString();
        }

        public override void Show()
        {
            base.Show();
            headerView.UpdatePage("Profile");
        }

        public override void Hide()
        {
            base.Hide();
            editProfileView.Hide();
            couponView.Hide();
        }


    }
}
