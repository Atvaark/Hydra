using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ChallengeCounter
    {
        [JsonProperty("Counter")]
        public ChallengeCounterId Counter { get; set; }

        [JsonProperty("Definition")]
        public string Definition { get; set; }

        [JsonProperty("Value")]
        public int Value { get; set; }

        [JsonProperty("Goal")]
        public int Goal { get; set; }
    }
}