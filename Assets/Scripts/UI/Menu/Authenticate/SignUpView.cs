using DynamicPixels.GameService.Services.Authentication.Models;
using Piranest.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.Auth
{
    public class SignUpView : View
    {
        [SerializeField] private TMP_InputField emailTxt, usernameTxt, passwordTxt, confirmPasswordTxt;

        [SerializeField] private Button confirmBtn, loginBtn;

        [SerializeField] private View loginView;

        [SerializeField] private AuthData authData;


        public override void InitView()
        {
            base.InitView();

            confirmBtn.onClick.RemoveAllListeners();
            confirmBtn.onClick.AddListener(SignUp);

            loginBtn.onClick.RemoveAllListeners();
            loginBtn.onClick.AddListener(() =>
            {
                loginView.Show();
                Hide();
            });

        }

        public (string, string) SignUpData()
        {
            return (emailTxt.text, passwordTxt.text);
        }

        public async void SignUp()
        {
            if (!IsValidate())
            {
                return;
            }
            var register = new RegisterWithEmailParams()
            {
                Email = emailTxt.text,
                Name = usernameTxt.text,
                Password = passwordTxt.text,
            };

            await authData.SignUp(register, (e) =>
            {
                Debug.Log($"Sign Up View Error:{e.Message}");
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
            if (string.IsNullOrWhiteSpace(usernameTxt.text))
            {
                PopUpManager.Instance.Show("Username Is Empty", () => { });
                return false;
            }
            if (passwordTxt.text != confirmPasswordTxt.text)
            {
                PopUpManager.Instance.Show("Password And Confirm Password Is Not Equal", () => { });
                return false;
            }

            return true;
        }

    }
}
