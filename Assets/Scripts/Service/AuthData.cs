using DynamicPixels.GameService;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.inputs;
using DynamicPixels.GameService.Services.Authentication.Models;
using DynamicPixels.GameService.Services.Table;
using DynamicPixels.GameService.Services.Table.Models;
using DynamicPixels.GameService.Services.User.Models;
using Piranest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(AuthData), menuName = Utility.SCRIPTABLE_PATH + nameof(AuthData))]
    public class AuthData : BaseServiceData
    {
        [field: SerializeField]
        public User User { get; set; }
        [field: SerializeField]
        public Account Account { get; set; }

        [field: SerializeField]
        public List<Coupon> Coupons { get; set; }

        public event Action<User> OnAuthSuccess;
        public event Action<User> OnUpdateUser;
        public event Action<Account> OnAccountChange;
        public event Action<List<Coupon>> OnCouponsChange;

        private const string ACCOUNT_TABLE_ID = "6550d82e75e62b435ba7451b";
        private const string VOUCHER_TABLE_ID = "6550d82e75e62b435ba7451d";

        public async Task SignUp(RegisterWithEmailParams register, Action<DynamicPixelsException> OnFail)
        {
            try
            {
                var response = await ServiceHub.Authentication.RegisterWithEmail(register);
                User = response.User;
                await GetAccount(User.Id);
                OnAuthSuccess?.Invoke(User);

            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);
                throw;
            }
        }

        public async Task Login(LoginWithEmailParams loginParam, Action<DynamicPixelsException> OnFail)
        {
            try
            {
                var response = await ServiceHub.Authentication.LoginWithEmail(loginParam);
                User = response.User;
                await GetAccount(User.Id);
                OnAuthSuccess?.Invoke(User);

            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);
                throw;
            }
        }

        public async Task UpdateUser(UserEditParams editParam, Action<DynamicPixelsException> OnFail)
        {
            try
            {
                var editCurrentParam = new EditCurrentUserParams()
                {
                    Data = editParam
                };
                var response = await ServiceHub.Services.Users.EditCurrentUser(editCurrentParam);
                User = response;
                OnUpdateUser?.Invoke(User);
            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);
                throw;
            }
        }

        public async Task GetAccount(int userId, Action<DynamicPixelsException> OnFail = null)
        {
            var findParam = new FindParams
            {
                tableId = ACCOUNT_TABLE_ID,
                options = new FindOptions
                {
                    Limit = 1,
                    Skip = 0,
                    Conditions = new Eq(Account.USER_ID, userId).ToQuery(),
                    Sorts = new Dictionary<string, Order>(),
                    Joins = new List<JoinParams>(),
                }
            };

            try
            {
                var response = await ServiceHub.Table.Find<Account, FindParams>(findParam);
                Account = response.List.Where(l => l.UserId == userId).FirstOrDefault();
                OnAccountChange?.Invoke(Account);
                await GetCoupons(userId);
            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);
                Debug.LogError($"Get Account:{e.Message}");
            }

        }

        public async Task GetCoupons(int userId, Action<DynamicPixelsException> OnFail = null)
        {
            var findParam = new FindParams
            {
                tableId = VOUCHER_TABLE_ID,
                options = new FindOptions
                {
                    Conditions = new Eq(Account.USER_ID, userId).ToQuery(),
                }
            };

            try
            {
                var response = await ServiceHub.Table.Find<Coupon, FindParams>(findParam);
                Coupons = response.List;
                OnCouponsChange?.Invoke(Coupons);

            }
            catch (DynamicPixelsException e)
            {
                OnFail?.Invoke(e);
                Debug.LogError($"Get Account:{e.Message}");
            }
        }

    }
}
