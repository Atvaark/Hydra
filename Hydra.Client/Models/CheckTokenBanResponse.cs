using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class CheckTokenBanResponse : ServiceResult
    {
        [JsonProperty("data")]
        public TokenBanData data { get; set; }
    }
}
