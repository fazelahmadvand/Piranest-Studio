using DynamicPixels.GameService.Services.Authentication.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Piranest.Auth
{
    public class LoginView : View
    {
        [SerializeField] private TMP_InputField emailTxt, passwordTxt;
        [SerializeField] private Button confirmBtn, signUpBtn;

        [SerializeField] private View signUpView;
        [SerializeField] private AuthData authData;
        public override void InitView()
        {
            base.InitView();
            confirmBtn.onClick.RemoveAllListeners();
            confirmBtn.onClick.AddListener(Login);



            signUpBtn.onClick.RemoveAllListeners();
            signUpBtn.onClick.AddListener(() =>
            {
                Hide();
                signUpView.Show();
            });
        }

        public (string, string) LoginData()
        {
            return (emailTxt.text, passwordTxt.text);
        }

        public async void Login()
        {
            var login = new LoginWithEmailParams()
            {
                email = emailTxt.text,
                password = passwordTxt.text,
            };
            await authData.Login(login, (e) =>
            {
                Debug.Log($"login View:{e.Message}", gameObject);
            });
        }

    }
}
