using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadInvitePresenceRequest
    {
        [JsonProperty("InviteId")]
        public string InviteId { get; set; }

        [JsonProperty("SquadId")]
        public SquadId SquadId { get; set; }

        [JsonProperty("UserIdFrom")]
        public UserId UserIdFrom { get; set; }

        [JsonProperty("UserIdTo")]
        public UserId UserIdTo { get; set; }
    }
}
