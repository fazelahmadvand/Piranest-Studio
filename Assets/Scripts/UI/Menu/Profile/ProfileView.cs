using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class ProfileView : View
    {
        [SerializeField] private Button editProfileButton;
        [SerializeField] private EditProfileView editProfileView;
        [SerializeField] private HeaerView headerView;

        public override void InitView()
        {
            base.InitView();
            editProfileView.InitView();
            editProfileButton.onClick.AddListener(() =>
            {
                editProfileView.Show();
                Hide();
                headerView.UpdateButton(() => 
                {
                    editProfileView.Hide();
                    Show();
                });
            });
        }
    }
}
