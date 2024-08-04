using DynamicPixels.GameService.Services.Authentication.Models;
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

        [SerializeField] private SaveData saveData;
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



    }
}
