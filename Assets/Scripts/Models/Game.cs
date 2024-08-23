using Newtonsoft.Json;
using UnityEngine;

namespace Piranest.Model
{
    [System.Serializable]
    public class Game
    {
        [JsonProperty("id")]
        [field: SerializeField]
        public int Id { get; set; }
        [JsonProperty("owner")]
        [field: SerializeField]
        public object Owner { get; set; }
        [JsonProperty("name")]
        [field: SerializeField]
        public string Name { get; set; }
        [JsonProperty("country")]
        [field: SerializeField]
        public string Country { get; set; }
        [JsonProperty("city")]
        [field: SerializeField]
        public string City { get; set; }
        [JsonProperty("story")]
        [field: SerializeField]
        public string Story { get; set; }
        [JsonProperty("time_limit")]
        [field: SerializeField]
        public int TimeLimit { get; set; }
        [JsonProperty("cover_image")]
        [field: SerializeField]
        public string CoverImageUrl { get; set; }
        [JsonProperty("details_image")]
        [field: SerializeField]
        public string DetailsImageUrl { get; set; }
        [JsonProperty("total_distance")]
        [field: SerializeField]
        public string TotalDistance { get; set; }
        [JsonProperty("prize")]
        [field: SerializeField]
        public int Prize { get; set; }
        [JsonProperty("rate")]
        [field: SerializeField]
        public object Rate { get; set; }
        [JsonProperty("meta")]
        [field: SerializeField]
        public object Meta { get; set; }


    }
}
