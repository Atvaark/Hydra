using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetLayersNewTokenRequest
    {
        [JsonProperty("structureVersion")]
        public string structureVersion { get; set; }

        [JsonProperty("newToken")]
        public string newToken { get; set; }

    }
}
