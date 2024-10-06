using Piranest.UI.Menu;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class CategoryGameView : View
    {
        [SerializeField] private List<Map<GameMode, Button>> games;
        [SerializeField] private HeaderView header;

        public override void InitView()
        {
            base.InitView();
            foreach (var game in games)
            {
                var gameMode = game.Key;
                var btn = game.Value;
                btn.SetEvent(() =>
                {
                    if (gameMode.CanHide())
                        Hide();
                    gameMode.Play();
                });
            }
        }

        public override void Show()
        {
            base.Show();
            header.UpdatePage("Categories");
            header.HandleBackButton(false);
        }



    }
}
