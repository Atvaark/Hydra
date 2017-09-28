using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetGameConfigLayersResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GameConfigLayerData[] data { get; set; }
    }
}