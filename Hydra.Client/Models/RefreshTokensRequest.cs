using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class RefreshTokensRequest
    {
        [JsonProperty("tokens")]
        public string[] tokens { get; set; }
    }
}