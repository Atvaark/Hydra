using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetTokensResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetTokensData data { get; set; }
    }
}