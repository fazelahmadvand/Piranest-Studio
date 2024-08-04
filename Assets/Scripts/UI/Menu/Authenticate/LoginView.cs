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
        [SerializeField] private SaveData saveData;

        public override void InitView()
        {
            base.InitView();

        }

    }
}
