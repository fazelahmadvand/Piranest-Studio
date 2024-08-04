using DynamicPixels.GameService.Services.Authentication.Models;
using DynamicPixels.GameService.Services.User.Models;
using Piranest.Auth;
using UnityEngine;

namespace Piranest
{
    public class AuthView : View
    {

        [SerializeField] private SignUpView signView;
        [SerializeField] private LoginView loginView;

        [SerializeField] private SaveData saveData;
        [SerializeField] private AuthData authData;

        public override async void InitView()
        {
            base.InitView();
            signView.InitView();
            loginView.InitView();

            LoadingHandler.Instance.Show();

            authData.OnLogin += OnLoginSuccess;
            authData.OnSignUp += OnSignUpSuccess;

            if (saveData.HasUser())
            {
                var info = saveData.SaveInfo;
                var loginParam = new LoginWithEmailParams()
                {
                    email = info.email,
                    password = info.password,
                };
                await authData.Login(loginParam, (e) =>
                {
                    Debug.Log($"---->Login Error: {e.Message}");
                });


            }
            else
            {
                signView.Show();
                LoadingHandler.Instance.Hide();

            }


        }

        private void OnDestroy()
        {
            authData.OnLogin -= OnLoginSuccess;
            authData.OnSignUp -= OnSignUpSuccess;

        }

        public override void Hide()
        {
            base.Hide();
            LoadingHandler.Instance.Hide();
            signView.Hide();
            loginView.Hide();
        }

        private void OnSignUpSuccess(User user)
        {
            Hide();
            var data = signView.SignUpData();
            saveData.LoginAndSignUpSaveUserData(data.Item1, data.Item2);
        }

        private void OnLoginSuccess(User user)
        {
            Hide();
            var data = loginView.LoginData();
            saveData.LoginAndSignUpSaveUserData(data.Item1, data.Item2);
        }
    }
}
