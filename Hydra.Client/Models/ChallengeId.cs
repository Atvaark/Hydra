using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ChallengeId
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}