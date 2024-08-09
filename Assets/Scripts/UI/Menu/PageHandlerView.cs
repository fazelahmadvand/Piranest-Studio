using DynamicPixels.GameService.Services.User.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class PageHandlerView : View
    {
        [SerializeField] private Image selectImg;
        [SerializeField] private List<FooterPage> footerPage;

        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            HideAll();
            foreach (var page in footerPage)
            {
                page.btn.onClick.AddListener(() =>
                {
                    HideAll();
                    page.view.Show();
                    selectImg.sprite = page.selectSprite;
                });

            }
            authData.OnAuthSuccess += OnInitialized;

        }

        private void OnDestroy()
        {
            authData.OnAuthSuccess -= OnInitialized;
        }

        private void OnInitialized(User user)
        {
            HideAll();
            footerPage[0].view.Show();
            selectImg.sprite = footerPage[0].selectSprite;
        }

        private void HideAll()
        {
            foreach (var page in footerPage)
                page.view.Hide();
        }

    }


    [System.Serializable]
    public struct FooterPage
    {
        public View view;
        public Button btn;
        public Sprite selectSprite;
    }
}
