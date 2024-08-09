using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class LeaderboardView : View
    {
        [SerializeField] private HeaderView headerView;

        public override void Show()
        {
            base.Show();
            headerView.UpdatePage("Leaderboard");
        }

    }
}
