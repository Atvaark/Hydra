using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetChallengeRequest
    {
        [JsonProperty("challenge")]
        public ChallengeId challenge { get; set; }
    }
}
