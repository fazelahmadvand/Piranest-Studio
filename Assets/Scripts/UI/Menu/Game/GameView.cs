using UnityEngine;

namespace Piranest.UI.Menu
{
    public class GameView : View
    {
        [SerializeField] private HeaderView headerView;

        [SerializeField] private AuthData authData;

        public override void InitView()
        {
            base.InitView();
        }

        public override void Show()
        {
            headerView.UpdatePage("Game");
            base.Show();
        }

    }
}
