using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class ProfileView : View
    {
        [SerializeField] private Button editProfileButton;
        [SerializeField] private EditProfileView editProfileView;
        [SerializeField] private HeaderView headerView;

        [Space]
        [SerializeField] private TMP_Text gemTxt;
        [SerializeField] private TMP_Text rankTxt;
        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            base.InitView();
            editProfileView.InitView();
            editProfileButton.onClick.AddListener(() =>
            {
                editProfileView.Show();
                Hide();
                headerView.UpdateHeader("Edit Profile", () =>
                {
                    editProfileView.Hide();
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

        private void OnAccountChange(Piranest.Model.Account account)
        {
            gemTxt.text = account.Remaining.ToString();
        }

        public override void Show()
        {
            base.Show();
            headerView.UpdatePage("Profile");
        }



    }
}
