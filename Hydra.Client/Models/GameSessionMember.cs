using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GameSessionMember
    {
        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("Party")]
        public PartyId Party { get; set; }

        [JsonProperty("Ready")]
        public bool Ready { get; set; }
    }
}