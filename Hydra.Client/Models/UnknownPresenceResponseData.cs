using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownPresenceResponseData
    {
        [JsonProperty("User")]
        public UserPresence User { get; set; }

        [JsonProperty("Squad")]
        public SquadPresence Squad { get; set; }

        [JsonProperty("Matchmake")]
        public MatchmakePresence Matchmake { get; set; }

        [JsonProperty("TournamentMatch")]
        public TournamentMatchPresence TournamentMatch { get; set; }

        [JsonProperty("Tournament")]
        public TournamentPresence Tournament { get; set; }
    }
}