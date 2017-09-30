using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ChallengesData
    {
        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("Value")]
        public ChallengesValue Value { get; set; }
    }
}