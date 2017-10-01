using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadInviteEvent
    {
        [JsonProperty("EventType")]
        public int EventType { get; set; }

        [JsonProperty("Data")]
        public SquadInviteEventData Data { get; set; }

        [JsonProperty("SequenceId")]
        public int SequenceId { get; set; }
    }
}