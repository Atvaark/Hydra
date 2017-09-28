using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetTokensResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GetTokensData data { get; set; }
    }
}