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
        [SerializeField] private GameObject gemObj;
        [SerializeField] private TMP_Text gemTxt;

        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            base.InitView();
            if (AuthData.HasUser)
            {
                OnInitialized();
                OnAccountChange(authData.Account);
            }
            authData.OnAccountChange += OnAccountChange;
            authData.OnAuthSuccess += OnAuthSuccess;

        }

        private void OnDestroy()
        {
            authData.OnAuthSuccess -= OnAuthSuccess;
            authData.OnAccountChange -= OnAccountChange;
        }

        public override void Show()
        {
            base.Show();
            HandleGem(true);
        }

        private void OnAuthSuccess(DynamicPixels.GameService.Services.User.Models.User obj)
        {
            OnInitialized();
            OnAccountChange(authData.Account);
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

        public void HandleGem(bool isActive)
        {
            gemObj.SetActive(isActive);
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
