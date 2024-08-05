using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class PageHandlerView : View
    {
        [SerializeField] private List<FooterPage> footerPage;

        public override void InitView()
        {
            foreach (var page in footerPage)
            {
                page.btn.UpdateButton(() =>
                {
                    HideAll();
                    page.page.Show();
                    page.btn.HandleBorder(true);
                });
            }



        }

        private void HideAll()
        {
            foreach (var page in footerPage)
            {
                page.page.Hide();
                page.btn.HandleBorder(false);
            }
        }

    }


    [System.Serializable]
    public struct FooterPage
    {
        public View page;
        public ButtonView btn;
    }
}
