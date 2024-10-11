using DynamicPixels.GameService.Services.Authentication.Models;
using Piranest.UI;
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
            confirmBtn.SetEvent(Login);

            signUpBtn.SetEvent(() =>
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
            if (!IsValidate())
            {
                return;
            }
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

        private bool IsValidate()
        {
            if (string.IsNullOrWhiteSpace(emailTxt.text))
            {
                PopUpManager.Instance.Show("Email Is Empty", () => { });
                return false;
            }
            if (string.IsNullOrWhiteSpace(passwordTxt.text))
            {
                PopUpManager.Instance.Show("Password Is Empty", () => { });
                return false;
            }
            return true;
        }

    }
}
