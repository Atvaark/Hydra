using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownTokenResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UnknownTokenResponseData data { get; set; }
    }
}