using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadPresence
    {
        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("Status")]
        public SquadStatus Status { get; set; }

        [JsonProperty("InviteEvents")]
        public SquadInviteEvent[] InviteEvents { get; set; }

        [JsonProperty("SquadEvents")]
        public object[] SquadEvents { get; set; }
    }
}