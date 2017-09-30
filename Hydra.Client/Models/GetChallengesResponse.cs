using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetChallengesResponse : ServiceResult
    {
        [JsonProperty("data")]
        public ChallengesData data { get; set; }
    }
}