using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class RefreshTokensResponse : ServiceResult
    {
        [JsonProperty("data")]
        public RefreshTokensData data { get; set; }
    }
}