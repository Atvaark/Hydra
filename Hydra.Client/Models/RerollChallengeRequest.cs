using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class RerollChallengeRequest
    {
        [JsonProperty("challenge")]
        public ChallengeId challenge { get; set; }
    }
}
