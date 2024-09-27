using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class PageHandlerView : View
    {
        [SerializeField] private Image selectImg;
        [SerializeField] private List<FooterPage> footerPage;
        [SerializeField] private FooterPageTypeEnum currentSelectedPage;

        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            Show();
            HideAll();
            foreach (var page in footerPage)
            {
                page.btn.onClick.AddListener(() =>
                {
                    ShowPage(page.type);
                });

            }
            OnInitialized();
            authData.OnAuthSuccess += OnAuthSuccess;
        }

        private void OnDestroy()
        {
            authData.OnAuthSuccess -= OnAuthSuccess;
        }
        private void OnAuthSuccess(DynamicPixels.GameService.Services.User.Models.User obj)
        {
            ShowPage(currentSelectedPage);
        }

        private void OnInitialized()
        {
            HideAll();
            ShowPage(FooterPageTypeEnum.Vendor);
        }

        private void HideAll()
        {
            foreach (var page in footerPage)
                page.view.Hide();
        }

        public void ShowPage(FooterPageTypeEnum type)
        {
            HideAll();
            var footer = footerPage.FirstOrDefault(f => f.type == type);
            footer.view.Show();
            selectImg.sprite = footer.selectSprite;
            currentSelectedPage = type;
        }



    }


    [System.Serializable]
    public struct FooterPage
    {
        public FooterPageTypeEnum type;
        public View view;
        public Button btn;
        public Sprite selectSprite;
    }

    public enum FooterPageTypeEnum
    {
        Game = 0,
        Vendor = 1,
        Leaderboard = 2,
        Profile = 3
    }


}
