using DynamicPixels.GameService;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Services.Authentication.Models;
using DynamicPixels.GameService.Services.Table.Models;
using DynamicPixels.GameService.Services.User.Models;
using Piranest.Model;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(AuthData), menuName = Utility.SCRIPTABLE_PATH + nameof(AuthData))]
    public class AuthData : BaseServiceData
    {

        public User User { get; set; }

        private const string PROFILE_TABLE_ID = "";
        public event Action<User> OnLogin;
        public event Action<User> OnSignUp;
        public event Action<User> OnUpdateUser;

        public async Task SignUp(RegisterWithEmailParams register, Action<DynamicPixelsException> OnFail)
        {
            try
            {
                var response = await ServiceHub.Authentication.RegisterWithEmail(register);
                User = response.User;
                OnSignUp?.Invoke(User);
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
                OnLogin?.Invoke(User);
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

        public async Task GetProfiles(Action<DynamicPixelsException> OnFail = null)
        {
            var findParam = new FindParams()
            {
                tableId = PROFILE_TABLE_ID,
                options = new()
            };

            try
            {
                //var response = await ServiceHub.Table.Find<Vendor, FindParams>(findParam);

            }
            catch (DynamicPixelsException e)
            {
                OnFail.Invoke(e);
                throw;
            }


        }

    }
}
