using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class ProfileView : View
    {
        [SerializeField] private View authView;
        [SerializeField] private CouponView couponView;
        [SerializeField] private Button editProfileButton, couponBtn;
        [SerializeField] private EditProfileView editProfileView;
        [SerializeField] private HeaderView headerView;

        [Space]
        [SerializeField] private TMP_Text gemTxt;
        [SerializeField] private TMP_Text rankTxt;

        [SerializeField] private AuthData authData;
        [SerializeField] private LeaderboardData leaderboardData;

        public override void InitView()
        {
            base.InitView();
            editProfileView.InitView();
            editProfileButton.SetEvent(() =>
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

            couponBtn.SetEvent(() =>
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
            OnGetLeaderboard();
            OnAccountChange(authData.Account);
            authData.OnAccountChange += OnAccountChange;
            leaderboardData.OnGetLeaderboard += OnGetLeaderboard;
        }

        private void OnDestroy()
        {
            authData.OnAccountChange -= OnAccountChange;
            leaderboardData.OnGetLeaderboard -= OnGetLeaderboard;
        }

        private void OnAccountChange(Model.Account account)
        {
            gemTxt.text = account.Remaining.ToString();
        }

        private void OnGetLeaderboard()
        {
            int rank = leaderboardData.AllAccounts.FindIndex(a => a.UserId == authData.User.Id);
            rank++;
            rankTxt.text = rank.ToString();
        }

        public override void Show()
        {
            if (AuthData.HasUser)
            {
                base.Show();
                headerView.UpdatePage("Profile");
            }
            else
            {
                authView.Show();
            }
        }

        public override void Hide()
        {
            base.Hide();
            editProfileView.Hide();
            couponView.Hide();
        }


    }
}
