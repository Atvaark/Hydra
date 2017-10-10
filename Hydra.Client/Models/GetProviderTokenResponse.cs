using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetProviderTokenResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetTokensData data { get; set; }
    }
}