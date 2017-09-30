using Newtonsoft.Json;

namespace Hydra.Client.Models.Unknown
{
    public class UnknownTokenResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UnknownTokenResponseData data { get; set; }
    }
    
    public class UnknownTokenResponseData
    {
        [JsonProperty("Token")]
        public string Token { get; set; }
    }

}
