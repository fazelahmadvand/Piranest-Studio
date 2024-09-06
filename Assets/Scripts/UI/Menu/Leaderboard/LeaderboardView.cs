using Piranest.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Piranest.UI.Menu
{
    public class LeaderboardView : View
    {
        [SerializeField] private HeaderView headerView;
        [SerializeField] private List<TopLeaderboardCardView> top3Cards = new();

        [Space]
        [SerializeField] private Transform cardHolder;
        [SerializeField] private LeaderboardCardView cardView;

        [Header("Services")]
        [SerializeField] private LeaderboardData leaderboardData;
        [SerializeField] private AuthData authData;


        private List<LeaderboardCardView> cards = new();
        public override void InitView()
        {
            base.InitView();
            CreateCards();
            leaderboardData.OnGetLeaderboard += HandleLeaderboard;
        }

        public override void Show()
        {
            base.Show();
            headerView.UpdatePage("Leaderboard");
        }

        private void OnDestroy()
        {
            leaderboardData.OnGetLeaderboard -= HandleLeaderboard;
        }

        private void CreateCards()
        {
            cardHolder.DestroyChildren();
            for (int i = 0; i < 5; i++)
            {
                var newCard = Instantiate(cardView, cardHolder);
                cards.Add(newCard);
            }
        }


        private async void HandleLeaderboard()
        {
            UpdateTop3();
            var all = leaderboardData.AllAcounts;
            int userIndex = all.FindIndex(a => a.UserId == authData.User.Id);

            bool isUserInTop3 = userIndex <= 2;
            bool isUseronBot3 = all.Count - 1 - 3 < userIndex;

            var list = new List<Account>();

            if (isUseronBot3)
                list = all.Take(5).ToList();
            else if (isUseronBot3)
                list = all.TakeLast(5).ToList();
            else
                list = all.Skip(userIndex - 2).Take(5).ToList();
            for (int i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var data = list[i];
                var user = await authData.GetUser(data.UserId);
                if (user == null) card.Hide();
                else
                {
                    int rank = all.IndexOf(data) + 1;
                    bool isLocalPlayer = data.UserId == authData.User.Id;
                    card.UpdateCard(rank, user.Name, data.Remaining, isLocalPlayer);
                }
            }
        }

        private async void UpdateTop3()
        {
            var top3 = leaderboardData.GetTop3();

            for (int i = 0; i < top3Cards.Count; i++)
            {
                var card = top3Cards[i];
                var data = top3[i];
                var user = await authData.GetUser(data.UserId);
                if (user != null)
                    card.UpdateCard(user.Name, data.Earned);
            }
        }
    }
}
