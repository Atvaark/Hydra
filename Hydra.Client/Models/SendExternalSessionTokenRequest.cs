using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendExternalSessionTokenRequest
    {
        [JsonProperty("externalSessionToken")]
        public string externalSessionToken { get; set; }
    }
}