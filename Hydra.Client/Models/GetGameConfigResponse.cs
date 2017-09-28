using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetGameConfigResponse : BaseResponse
    {
        [JsonProperty("data")]
        public GameConfigLayer[] data { get; set; }
    }
}