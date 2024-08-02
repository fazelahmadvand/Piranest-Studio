using DynamicPixels.GameService;
using DynamicPixels.GameService.Services.Authentication.Models;
using DynamicPixels.GameService.Services.User.Models;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Piranest
{
    [CreateAssetMenu(fileName = nameof(AuthData), menuName = Utility.SCRIPTABLE_PATH + nameof(AuthData))]
    public class AuthData : BaseServiceData
    {

        public User User { get; set; }

        public async Task SignUp(RegisterWithEmailParams register, Action<User> OnSuccess, Action OnFail)
        {
            try
            {
                var response = await ServiceHub.Authentication.RegisterWithEmail(register);
                User = response.User;
                OnSuccess?.Invoke(response.User);
            }
            catch (System.Exception)
            {
                OnFail?.Invoke();
                throw;
            }
        }

        public async Task Login(LoginWithEmailParams loginParam, Action<User> OnSuccess, Action OnFail)
        {
            try
            {
                var response = await ServiceHub.Authentication.LoginWithEmail(loginParam);
                User = response.User;
                OnSuccess?.Invoke(response.User);
            }
            catch (Exception)
            {
                OnFail?.Invoke();
                throw;
            }
        }

        public async Task UpdateUser(UserEditParams editParam, Action<User> OnSuccess, Action OnFail)
        {
            try
            {
                var editCurrentParam = new EditCurrentUserParams()
                {
                    Data = editParam
                };
                var response = await ServiceHub.Services.Users.EditCurrentUser(editCurrentParam);
                User = response;
                OnSuccess?.Invoke(response);
            }
            catch (Exception)
            {
                OnFail?.Invoke();
                throw;
            }
        }

    }
}
