using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadStatusSettings
    {
        [JsonProperty("MaxCount")]
        public int MaxCount { get; set; }

        [JsonProperty("InviteDelegation")]
        public int InviteDelegation { get; set; }
    }
}