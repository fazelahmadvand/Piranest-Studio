using Newtonsoft.Json;
using UnityEngine;
namespace Piranest.Model
{
    [System.Serializable]
    public class Vendor
    {
        [JsonProperty("id")]
        [field: SerializeField]
        public int Id { get; set; }
        [field: SerializeField]
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [field: SerializeField]
        [JsonProperty("name")]
        public string Name { get; set; }
        [field: SerializeField]
        [JsonProperty("vendor_category_id ")]
        public int VendorCategoryId { get; set; }
        [field: SerializeField]
        [JsonProperty("image")]
        public string ImageUrl { get; set; }
        [field: SerializeField]
        [JsonProperty("city")]
        public string City { get; set; }
        [field: SerializeField]
        [JsonProperty("location_lat")]
        public float LocationLat { get; set; }
        [field: SerializeField]
        [JsonProperty("location_long")]
        public float LocationLong { get; set; }
        [field: SerializeField]
        [JsonProperty("max_coupon")]
        public int MaxCoupon { get; set; }


    }
}
