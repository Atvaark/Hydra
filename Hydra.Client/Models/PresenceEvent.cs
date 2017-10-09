using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class PresenceEvent
    {
        [JsonProperty("Created")]
        public string Created { get; set; }

        [JsonProperty("SequenceId")]
        public int SequenceId { get; set; }

        [JsonProperty("EventType")]
        public int EventType { get; set; }
    }
}