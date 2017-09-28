using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class RefreshTokensResponse : BaseResponse
    {
        [JsonProperty("data")]
        public RefreshTokensData data { get; set; }
    }
}