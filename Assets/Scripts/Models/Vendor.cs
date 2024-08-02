using Newtonsoft.Json;
using UnityEngine;
namespace Piranest.Model
{
    [System.Serializable]
    public class Vendor
    {
        [JsonProperty("id")]
        [field: SerializeField]
        public object Id { get; set; }
        [field: SerializeField]
        [JsonProperty("owner")]
        public object Owner { get; set; }
        [field: SerializeField]
        [JsonProperty("name")]
        public object Name { get; set; }
        [field: SerializeField]
        [JsonProperty("vendor_category_id ")]
        public object VendorCategoryId { get; set; }
        [field: SerializeField]
        [JsonProperty("image")]
        public object Image { get; set; }
        [field: SerializeField]
        [JsonProperty("city")]
        public object City { get; set; }
        [field: SerializeField]
        [JsonProperty("location_lat")]
        public object LocationLat { get; set; }
        [field: SerializeField]
        [JsonProperty("location_long")]
        public object LocationLong { get; set; }
        [field: SerializeField]
        [JsonProperty("max_coupon")]
        public object MaxCoupon { get; set; }


    }
}
