using DynamicPixels.GameService.Services.User.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Piranest.UI.Menu.Header
{
    public class HeaerView : View
    {

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


    }
}
