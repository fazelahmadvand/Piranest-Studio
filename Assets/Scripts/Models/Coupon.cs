using Newtonsoft.Json;
using UnityEngine;

namespace Piranest.Model
{
    [System.Serializable]
    public class Coupon
    {
        [field: SerializeField]
        [JsonProperty("id")]
        public int Id { get; set; }
        [field: SerializeField]
        [JsonProperty("owner")] public object Owner { get; set; }
        [field: SerializeField]
        [JsonProperty("vendour_coupen_id")] public string VendourCouponId { get; set; }
        [field: SerializeField]
        [JsonProperty("user_id")] public int UserId { get; set; }
        [field: SerializeField]
        [JsonProperty("amount")] public int Amount { get; set; }
        [field: SerializeField]
        [JsonProperty("currency_id")] public int CurrencyId { get; set; }
        [field: SerializeField]
        [JsonProperty("code")] public string Code { get; set; }


    }
}
