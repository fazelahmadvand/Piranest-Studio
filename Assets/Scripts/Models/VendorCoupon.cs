using Newtonsoft.Json;
using UnityEngine;

namespace Piranest.Model
{
    [System.Serializable]
    public class VendorCoupon
    {
        [field: SerializeField]
        [JsonProperty("id")]
        public int Id { get; set; }
        [field: SerializeField]
        [JsonProperty("discount_percentage")] public int DiscountPercentage { get; set; }
        [field: SerializeField]
        [JsonProperty("max_percentage_amount")] public int MaxPercentageAmount { get; set; }
        [field: SerializeField]
        [JsonProperty("max_issued")] public int MaxIssued { get; set; }
        [field: SerializeField]
        [JsonProperty("owner")] public object Owner { get; set; }
        [field: SerializeField]
        [JsonProperty("vendor_id")] public int VendorId { get; set; }
        [field: SerializeField]
        [JsonProperty("price_amount")] public int PriceAmount { get; set; }
        [field: SerializeField]
        [JsonProperty("price_currency_id")] public int PriceCurrencyId { get; set; }
        [field: SerializeField]
        [JsonProperty("discount_amount")] public int DiscountAmount { get; set; }


    }
}
