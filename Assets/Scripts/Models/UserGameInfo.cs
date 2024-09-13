using Newtonsoft.Json;
using UnityEngine;

namespace Piranest.Model
{
    [System.Serializable]
    public class UserGameInfo
    {
        [field: SerializeField]
        [JsonProperty("id")]
        public int Id { get; set; }
        [field: SerializeField]
        [JsonProperty("owner")]
        public int Owner { get; set; }
        [field: SerializeField]
        [JsonProperty("game_id")]
        public int GameId { get; set; }
        [field: SerializeField]
        [JsonProperty("chapter_id")]
        public int ChapterId { get; set; }
        [field: SerializeField]
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }
        [field: SerializeField]
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [field: SerializeField]
        [JsonProperty("is_answer_true")]
        public bool IsAnswerTrue { get; set; }

    }
}
