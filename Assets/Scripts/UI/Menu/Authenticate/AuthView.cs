using DynamicPixels.GameService.Services.Authentication.Models;
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

            Manager.Instance.OnInitialized += OnInitialized;
            authData.OnAuthSuccess += OnLoginSuccess;
            authData.OnAuthSuccess += OnSignUpSuccess;

        }

        private void OnDestroy()
        {
            if (Manager.Instance)
                Manager.Instance.OnInitialized -= OnInitialized;
            authData.OnAuthSuccess -= OnLoginSuccess;
            authData.OnAuthSuccess -= OnSignUpSuccess;
        }

        public override void Hide()
        {
            base.Hide();
            LoadingHandler.Instance.Hide();
            signView.Hide();
            loginView.Hide();
        }

        private async void OnInitialized()
        {
            if (userSaveData.HasUser())
            {
                var data = userSaveData.Data;
                var loginParam = new LoginWithEmailParams()
                {
                    email = data.email,
                    password = data.password,
                };
                await authData.Login(loginParam, (e) =>
                {
                    Debug.Log($"---->Login Error: {e.Message}");
                });
                await authData.GetProfiles();

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
            var data = signView.SignUpData();
            userSaveData.LoginAndSignUpSaveUserData(data.Item1, data.Item2 );
        }

        private void OnLoginSuccess(User user)
        {
            Hide();
            var data = loginView.LoginData();
            userSaveData.LoginAndSignUpSaveUserData(data.Item1, data.Item2);
        }
    }
}
