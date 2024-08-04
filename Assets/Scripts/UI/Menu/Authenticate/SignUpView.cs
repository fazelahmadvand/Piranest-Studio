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



        public override void InitView()
        {
            base.InitView();
        }







    }
}
