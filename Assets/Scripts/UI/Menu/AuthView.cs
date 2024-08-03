using DynamicPixels.GameService.Services.User.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Piranest
{
    public class AuthView : View
    {
        [SerializeField] private AuthData authData;






        public override void InitView()
        {
            base.InitView();



            authData.OnLogin += OnLoginSuccess;

        }

        private void OnDestroy()
        {
            authData.OnLogin -= OnLoginSuccess;

        }


        private void OnLoginSuccess(User user)
        {
            //panel animation
            //panel disable
            //menu main icon click
        }
    }
}
