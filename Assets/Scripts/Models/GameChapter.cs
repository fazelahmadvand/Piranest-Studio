using Newtonsoft.Json;
using UnityEngine;

namespace Piranest.Model
{
    [System.Serializable]
    public class GameChapter
    {
        [field: SerializeField]
        [JsonProperty("id")]
        public int Id { get; set; }
        [field: SerializeField]
        [JsonProperty("owner")]
        public object Owner { get; set; }
        [field: SerializeField]
        [JsonProperty("title")]
        public string Title { get; set; }
        [field: SerializeField]
        [JsonProperty("media")]
        public string MediaUrl { get; set; }
        [field: SerializeField]
        [JsonProperty("sort_number")]
        public int SortNumber { get; set; }
        [field: SerializeField]
        [JsonProperty("game_id")]
        public int GameId { get; set; }
        [field: SerializeField]
        [JsonProperty("description")]
        public string Description { get; set; }


    }
}
