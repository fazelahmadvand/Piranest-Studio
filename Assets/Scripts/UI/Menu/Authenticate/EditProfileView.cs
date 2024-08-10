using DynamicPixels.GameService.Services.User.Models;
using Piranest.SaveSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI.Menu
{
    public class EditProfileView : View
    {
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_Text usernameText , usernameTextInProfile;
        [SerializeField] private Button saveChangesBtn, cancelBtn;
        [SerializeField] private AuthData authData;
        [SerializeField] private UserSaveData userSaveData;

        public override void InitView()
        {
            base.InitView();
            saveChangesBtn.onClick.RemoveAllListeners();
            saveChangesBtn.onClick.AddListener(UpdateUsername);
            cancelBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.AddListener(() => Hide());
            authData.OnAuthSuccess += SetUserName;
            authData.OnUpdateUser += OnUserUpdate;
        }

        private void OnDestroy()
        {
            authData.OnAuthSuccess -= SetUserName;
            authData.OnUpdateUser -= OnUserUpdate;
        }
        private void SetUserName(User user)
        {
            usernameText.text = user.Username;
            usernameTextInProfile.text = user.Username;
        }

        private void OnUserUpdate(User user)
        {
            usernameText.text = user.Username;
            usernameTextInProfile.text = user.Username;
        }

        public async void UpdateUsername()
        {
            var username = usernameInputField.text;

            var editParams = new UserEditParams() { Username = username };

            try
            {
                // Update on server
                await authData.UpdateUser(editParams, (e) =>
                {
                    Debug.LogError("Update Username Error: " + e.Message);

                });
            }
            catch (Exception e)
            {
                Debug.LogError("Exception in UpdateUsername: " + e.Message);
            }
        }
    }
}
