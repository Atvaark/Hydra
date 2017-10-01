using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UsersClientStateData
    {
        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("IsInvitable")]
        public bool IsInvitable { get; set; }

        [JsonProperty("ClientState")]
        public int ClientState { get; set; }
    }
}