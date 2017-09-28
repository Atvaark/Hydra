using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GameConfigLayerData
    {
        [JsonProperty("Layer")]
        public GameConfigLayer Layer { get; set; }

        [JsonProperty("Classes")]
        public GameConfigLayerClass[] Classes { get; set; }
    }
}