using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TournamentPlayer
    {
        [JsonProperty("UserId")]
        public UserId UserId { get; set; }

        [JsonProperty("TournamentUserId")]
        public string TournamentUserId { get; set; }

        [JsonProperty("IsJoined")]
        public bool IsJoined { get; set; }

        [JsonProperty("Color")]
        public int Color { get; set; }

        [JsonProperty("Role")]
        public int Role { get; set; }
    }
}