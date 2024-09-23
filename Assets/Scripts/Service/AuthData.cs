using DynamicPixels.GameService;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.inputs;
using DynamicPixels.GameService.Services.Authentication.Models;
using DynamicPixels.GameService.Services.Table;
using DynamicPixels.GameService.Services.Table.Models;
using DynamicPixels.GameService.Services.User.Models;
using Piranest.Model;
using Piranest.SaveSystem;
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

        [SerializeField] private UserSaveData userSaveData;

        public event Action<User> OnAuthSuccess;
        public event Action<User> OnUpdateUser;
        public event Action<Account> OnAccountChange;
        public event Action<List<Coupon>> OnCouponsChange;
        public event Action<string> OnCouponCreate;

        private const string ACCOUNT_TABLE_ID = "6550d82e75e62b435ba7451b";
        private const string VOUCHER_TABLE_ID = "6550d82e75e62b435ba7451d";
        private const string USER_TABLE_ID = "652520bbb6aed5393bb0dc8a";

        public static bool HasUser { get; private set; }


        public bool CanBuy(int costAmount)
        {
            return Account.Remaining >= costAmount;
        }

        public override async Task Init(Action<DynamicPixelsException> OnFail)
        {
            HasUser = userSaveData.HasUser();
            if (userSaveData.HasUser())
            {
                var data = userSaveData.Data;
                var loginParam = new LoginWithEmailParams()
                {
                    email = data.email,
                    password = data.password,
                };
                await Login(loginParam, (e) =>
                {
                    OnFail?.Invoke(e);
                    Debug.Log($"---->Login Error: {e.Message}");
                });
            }
        }

        public async Task SignUp(RegisterWithEmailParams register, Action<DynamicPixelsException> OnFail)
        {
            try
            {
                var response = await ServiceHub.Authentication.RegisterWithEmail(register);
                User = response.User;
                await CreateAccount();
                HasUser = true;
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
                HasUser = true;
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

        private async Task CreateAccount()
        {
            var newAccount = new Account()
            {
                Id = Mathf.Abs(Guid.NewGuid().GetHashCode()),
                CurrencyId = 1,
                Earned = 200,
                Spent = 0,
                Remaining = 200,
                UserId = User.Id,
                Hashedvalue = ""
            };
            var insertParam = new InsertParams()
            {
                Data = newAccount,
                TableId = ACCOUNT_TABLE_ID,
            };
            try
            {
                var response = await ServiceHub.Table.Insert<Account, InsertParams>(insertParam);
                Account = newAccount;
                OnAccountChange?.Invoke(newAccount);
            }
            catch (DynamicPixelsException e)
            {
                Debug.Log("Create Account: " + e.Message);
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
                if (Account != null)
                    OnAccountChange?.Invoke(Account);
                else
                    await CreateAccount();
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

        public async Task<User> GetUser(int userId)
        {
            var find = new FindByIdParams
            {
                RowId = userId,
                TableId = USER_TABLE_ID
            };
            try
            {
                var res = await ServiceHub.Table.FindById<User, FindByIdParams>(find);
                return res.Row;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task UpdateAccount(int amount)
        {
            Account.Earned += amount;
            Account.Remaining += amount;
            var findById = new FindByIdAndUpdateParams()
            {
                Data = Account,
                RowId = Account.Id,
                TableId = ACCOUNT_TABLE_ID,
            };
            try
            {
                var response = await ServiceHub.Table.FindByIdAndUpdate<Account, FindByIdAndUpdateParams>(findById);
                OnAccountChange?.Invoke(Account);
            }
            catch (DynamicPixelsException e)
            {
                Debug.Log($"Update Account: {e.Message}");
            }
        }

        public async Task CreateCoupon(int vendorId, int couponAmount, Action<DynamicPixelsException> OnFail = null)
        {
            await UpdateAccount(couponAmount);
            string couponCode = Utility.GenerateRandomString();
            var newCoupon = new Coupon()
            {
                Id = Mathf.Abs(Guid.NewGuid().GetHashCode()),
                CurrencyId = 1,
                UserId = User.Id,
                Amount = Mathf.Abs(couponAmount),
                Code = couponCode,
                VendourCouponId = vendorId.ToString(),

            };
            var insertParam = new InsertParams()
            {
                Data = newCoupon,
                TableId = VOUCHER_TABLE_ID,
            };
            try
            {
                var response = await ServiceHub.Table.Insert<Coupon, InsertParams>(insertParam);
                Coupons.Add(newCoupon);
                OnCouponsChange?.Invoke(Coupons);
                OnCouponCreate?.Invoke(couponCode);

            }
            catch (DynamicPixelsException e)
            {
                Debug.Log("Create Coupon: " + e.Message);
                OnFail?.Invoke(e);
            }
        }


    }
}
