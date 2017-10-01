using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetChallenges2Response : ServiceResult
    {
        [JsonProperty("data")]
        public ChallengesData data { get; set; }
    }
}
