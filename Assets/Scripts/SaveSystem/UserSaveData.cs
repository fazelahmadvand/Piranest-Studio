using UnityEngine;

namespace Piranest.SaveSystem
{
    [CreateAssetMenu(fileName = nameof(UserSaveData), menuName = Utility.SCRIPTABLE_PATH + nameof(UserSaveData))]

    public class UserSaveData : SaveData<SaveInfo>
    {
        public bool HasUser()
        {
            return !string.IsNullOrWhiteSpace(Data.email) && !string.IsNullOrWhiteSpace(Data.password);
        }

        public void LoginAndSignUpSaveUserData(string email, string pass)
        {
            if (HasUser()) return;
            Data = new SaveInfo()
            {
                email = email,
                password = pass,
            };
            Save();
        }

        protected override string KeyName()
        {
            return "UserSaveData";
        }

        [ContextMenu("Delete Data")]
        public void DeleteKey()
        {
            if (PlayerPrefs.HasKey(KeyName()))
                PlayerPrefs.DeleteKey(KeyName());
        }
    }

    [System.Serializable]
    public struct SaveInfo
    {
        public string email;
        public string password;
    }
}
