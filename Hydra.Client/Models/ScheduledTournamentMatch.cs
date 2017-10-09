using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ScheduledTournamentMatch
    {
        [JsonProperty("TournamentMatchId")]
        public string TournamentMatchId { get; set; }

        [JsonProperty("LobyStartTimeUTC")]
        public int LobyStartTimeUTC { get; set; }

        [JsonProperty("MatchStartTimeUTC")]
        public int MatchStartTimeUTC { get; set; }
        
        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}