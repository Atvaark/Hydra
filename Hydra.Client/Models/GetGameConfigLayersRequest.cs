using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetGameConfigLayersRequest
    {
        [JsonProperty("layers")]
        public GameConfigLayer[] layers { get; set; }
    }
}