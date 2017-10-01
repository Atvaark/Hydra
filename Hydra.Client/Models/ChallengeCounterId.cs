using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ChallengeCounterId
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}