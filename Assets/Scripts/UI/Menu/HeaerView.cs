using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class HeaerView : View
    {
        [SerializeField] private Button backBtn;
        [SerializeField] private TMP_Text gemTxt;

        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            base.InitView();
            Manager.Instance.OnInitialized += OnInitialized;
        }

        private void OnDestroy()
        {
            if (Manager.Instance)
                Manager.Instance.OnInitialized -= OnInitialized;
        }

        private void OnInitialized()
        {
            Show();
        }

        public void UpdateButton(Action OnClick)
        {
            backBtn.onClick.RemoveAllListeners();
            backBtn.onClick.AddListener(() => OnClick?.Invoke());
        }
    }
}
