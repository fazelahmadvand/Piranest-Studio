using Newtonsoft.Json;
using UnityEngine;
namespace Piranest.Model
{
    [System.Serializable]
    public class Account
    {
        [field: SerializeField]
        [JsonProperty("id")]
        public int Id { get; set; }

        [field: SerializeField]
        [JsonProperty("owner")]
        public int Owner { get; set; }

        [field: SerializeField]
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [field: SerializeField]
        [JsonProperty("currency_id")]
        public int CurrencyId { get; set; }

        [field: SerializeField]
        [JsonProperty("earned")]
        public int Earned { get; set; }

        [field: SerializeField]
        [JsonProperty("spent")]
        public int Spent { get; set; }

        [field: SerializeField]
        [JsonProperty("remaining")]
        public int Remaining { get; set; }

        [field: SerializeField]
        [JsonProperty("hashedvalue")]
        public string Hashedvalue { get; set; }


        public const string USER_ID = "user_id";
    }
}
