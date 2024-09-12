using DynamicPixels.GameService.Services.User.Models;
using Piranest.Auth;
using Piranest.SaveSystem;
using UnityEngine;

namespace Piranest
{
    public class AuthView : View
    {

        [SerializeField] private SignUpView signView;
        [SerializeField] private LoginView loginView;

        [SerializeField] private UserSaveData userSaveData;
        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            Show();
            signView.InitView();
            loginView.InitView();

            Manager.OnInitialized += OnInitialized;
            authData.OnAuthSuccess += OnLoginSuccess;
            authData.OnAuthSuccess += OnSignUpSuccess;

        }

        private void OnDestroy()
        {
            Manager.OnInitialized -= OnInitialized;
            authData.OnAuthSuccess -= OnLoginSuccess;
            authData.OnAuthSuccess -= OnSignUpSuccess;
        }


        private void OnInitialized()
        {
            if (userSaveData.HasUser())
            {

            }
            else
            {
                signView.Show();
                LoadingHandler.Instance.Hide();
            }
        }

        private void OnSignUpSuccess(User user)
        {
            Hide();
            signView.Hide();
            loginView.Hide();
            var data = signView.SignUpData();
            userSaveData.LoginAndSignUpSaveUserData(data.Item1, data.Item2);
        }

        private void OnLoginSuccess(User user)
        {
            Hide();
            signView.Hide();
            loginView.Hide();
            var data = loginView.LoginData();
            userSaveData.LoginAndSignUpSaveUserData(data.Item1, data.Item2);
        }
    }
}
