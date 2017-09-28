using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GameConfigLayerClass
    {
        [JsonProperty("ClassName")]
        public string ClassName { get; set; }

        [JsonProperty("ClassInstancesJson")]
        public string ClassInstancesJson { get; set; }
    }
}