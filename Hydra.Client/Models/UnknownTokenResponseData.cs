using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownTokenResponseData
    {
        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
