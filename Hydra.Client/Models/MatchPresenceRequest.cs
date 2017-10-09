using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class MatchPresenceRequest
    {
        [JsonProperty("mode")]
        public string mode { get; set; }

        [JsonProperty("map")]
        public string map { get; set; }

        [JsonProperty("isTeam")]
        public bool isTeam { get; set; }

        [JsonProperty("maxPlayers")]
        public int maxPlayers { get; set; }

        [JsonProperty("userTeams")]
        public MatchPresenceUserTeam[] userTeams { get; set; }

        [JsonProperty("dcPings")]
        public DataCenterPing[] dcPings { get; set; }
    }
}
