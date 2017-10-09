using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TournamentMatchStatus
    {
        [JsonProperty("MatchId")]
        public string MatchId { get; set; }

        [JsonProperty("GameSession")]
        public GameSessionId GameSession { get; set; }

        [JsonProperty("GameSessionContext")]
        public string GameSessionContext { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("Teams")]
        public TournamentTeam[] Teams { get; set; }

        [JsonProperty("Players")]
        public TournamentPlayer[] Players { get; set; }

        [JsonProperty("MatchConfig")]
        public TournamentMatchConfig MatchConfig { get; set; }

        [JsonProperty("LobbyCountDown")]
        public int LobbyCountDown { get; set; }
    }
}