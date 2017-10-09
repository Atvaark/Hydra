using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownAuthTokenRequest
    {
        [JsonProperty("authToken")]
        public string authToken { get; set; }

        [JsonProperty("versions")]
        public HydraServiceVersion[] versions { get; set; }
    }
}
