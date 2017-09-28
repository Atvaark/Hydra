using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetTokensRequest
    {
        [JsonProperty("token")]
        public string token { get; set; }

        [JsonProperty("versions")]
        public HydraServiceVersion[] versions { get; set; }
    }
}