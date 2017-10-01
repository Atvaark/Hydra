using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadStatus
    {
        [JsonProperty("GameData")]
        public string GameData { get; set; }

        [JsonProperty("SquadMembers")]
        public SquadMember[] SquadMembers { get; set; }

        [JsonProperty("Settings")]
        public SquadStatusSettings Settings { get; set; }
    }
}