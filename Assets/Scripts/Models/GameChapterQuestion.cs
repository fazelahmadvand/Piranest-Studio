using Newtonsoft.Json;
using UnityEngine;
namespace Piranest.Model
{
    [System.Serializable]
    public class GameChapterQuestion
    {
        [field: SerializeField]
        [JsonProperty("id")]
        public int Id { get; set; }
        [field: SerializeField]
        [JsonProperty("owner")]
        public object Owner { get; set; }
        [field: SerializeField]
        [JsonProperty("game_chapter_id")]
        public int GameChapterId { get; set; }
        [field: SerializeField]
        [JsonProperty("story")]
        public string Story { get; set; }
        [field: SerializeField]
        [JsonProperty("question")]
        public string Question { get; set; }
        [field: SerializeField]
        [JsonProperty("question_type")]
        public string QuestionType { get; set; }
        [field: SerializeField]
        [JsonProperty("options")]
        public string Options { get; set; }
        [field: SerializeField]
        [JsonProperty("right_answer")]
        public string RightAnswer { get; set; }
        [field: SerializeField]
        [JsonProperty("media")]
        public string MediaUrl { get; set; }
        [field: SerializeField]
        [JsonProperty("prize")]
        public int Prize { get; set; }
        [field: SerializeField]
        [JsonProperty("description")]
        public string Description { get; set; }
        [field: SerializeField]
        [JsonProperty("location_long")]
        public float LocationLong { get; set; }
        [field: SerializeField]
        [JsonProperty("location_lat")]
        public float LocationLat { get; set; }
        [field: SerializeField]
        [JsonProperty("sort_number")]
        public int SortNumber { get; set; }
        [field: SerializeField]
        [JsonProperty("answer_media")]
        public string AnswerMediaUrl { get; set; }
        [field: SerializeField]
        [JsonProperty("location_radius")]
        public float LocationRadius { get; set; }

    }
}
