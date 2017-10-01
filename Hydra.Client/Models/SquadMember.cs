using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadMember
    {
        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("IsOwner")]
        public bool IsOwner { get; set; }

        [JsonProperty("Penalty")]
        public int Penalty { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }
    }
}