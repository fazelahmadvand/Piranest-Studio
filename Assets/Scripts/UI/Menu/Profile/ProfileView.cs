using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class ProfileView : View
    {
        [SerializeField] private Button editProfileButton;
        [SerializeField] private EditProfileView editProfileView;
        [SerializeField] private HeaderView headerView;


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
        }

        public override void Show()
        {
            base.Show();
            headerView.UpdatePage("Profile");
        }



    }
}
