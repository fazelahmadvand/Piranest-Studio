using Piranest.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class HeaderView : View
    {
        [SerializeField] private TMP_Text headerTxt;
        [SerializeField] private Button backBtn;
        [SerializeField] private TMP_Text gemTxt;

        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            base.InitView();
            //Manager.Instance.OnInitialized += OnInitialized;
            authData.OnAccountChange += OnAccountChange;
            authData.OnAuthSuccess += OnAuthSuccess;

        }

        private void OnDestroy()
        {
            authData.OnAuthSuccess -= OnAuthSuccess;
            //if (Manager.Instance)
            //    Manager.Instance.OnInitialized -= OnInitialized;
            authData.OnAccountChange -= OnAccountChange;

        }

        private void OnAuthSuccess(DynamicPixels.GameService.Services.User.Models.User obj)
        {
            OnInitialized();
        }

        private void OnInitialized()
        {
            Show();
            HandleBackButton(false);
        }

        private void OnAccountChange(Account account)
        {
            gemTxt.text = account.Remaining.ToString();
        }


        public void UpdatePage(string pageName)
        {
            headerTxt.text = pageName;
        }

        public void UpdateHeader(string pageName, Action OnBackClick)
        {
            HandleBackButton(false);
            UpdatePage(pageName);
            UpdateButton(OnBackClick);
        }

        public void UpdateButton(Action OnClick)
        {
            HandleBackButton(true);
            backBtn.onClick.RemoveAllListeners();
            backBtn.onClick.AddListener(() => OnClick?.Invoke());
        }

        public void HandleBackButton(bool isActive)
        {
            backBtn.gameObject.SetActive(isActive);
        }

    }
}
