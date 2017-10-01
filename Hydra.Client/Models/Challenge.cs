using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class Challenge
    {
        [JsonProperty("Id")]
        public ChallengeId Id { get; set; }

        [JsonProperty("Definition")]
        public string Definition { get; set; }

        [JsonProperty("Type")]
        public int Type { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("Slot")]
        public int Slot { get; set; }

        [JsonProperty("Counters")]
        public ChallengeCounter[] Counters { get; set; }
    }
}