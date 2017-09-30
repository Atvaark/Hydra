using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetGameConfigLayersResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GameConfigLayerData[] data { get; set; }
    }
}