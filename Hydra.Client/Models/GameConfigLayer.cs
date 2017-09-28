using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GameConfigLayer
    {
        [JsonProperty("LayerName")]
        public string LayerName { get; set; }

        [JsonProperty("Hash")]
        public string Hash { get; set; }

        [JsonProperty("UpdatePolicy")]
        public int UpdatePolicy { get; set; }
    }
}