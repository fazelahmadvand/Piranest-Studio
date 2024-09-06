using DynamicPixels.GameService;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Services.Table;
using DynamicPixels.GameService.Services.Table.Models;
using Piranest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(LeaderboardData), menuName = Utility.SCRIPTABLE_PATH + nameof(LeaderboardData))]
    public class LeaderboardData : BaseServiceData
    {

        [field: SerializeField]
        public List<Account> AllAcounts { get; set; }

        private const string ACCOUNT_TABLE_ID = "6550d82e75e62b435ba7451b";

        public event Action OnGetLeaderboard;

        public override async Task Init()
        {
            await GetLeaderboard();
        }

        public async Task GetLeaderboard(Action<DynamicPixelsException> OnFail = null)
        {
            var findParam = new FindParams
            {
                tableId = ACCOUNT_TABLE_ID,
                options = new FindOptions(),
            };

            try
            {
                var response = await ServiceHub.Table.Find<Account, FindParams>(findParam);
                AllAcounts = response.List;
                AllAcounts = AllAcounts.OrderByDescending(a => a.Earned).ToList();
                OnGetLeaderboard?.Invoke();
            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);
                Debug.LogError($"GetLeaderboard:{e.Message}");
            }
        }

        public List<Account> GetTop3()
        {
            return AllAcounts.Take(3).ToList();
        }




    }
}
