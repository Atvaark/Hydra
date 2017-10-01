using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class PartyMember
    {
        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("IsOwner")]
        public bool IsOwner { get; set; }
    }
}