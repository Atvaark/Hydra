using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetGameConfigRequest
    {
        [JsonProperty("structureVersion")]
        public string structureVersion { get; set; }
    }
}