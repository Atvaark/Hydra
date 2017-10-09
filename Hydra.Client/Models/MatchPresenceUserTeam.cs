using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class MatchPresenceUserTeam
    {
        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("Team")]
        public int Team { get; set; }
    }
}