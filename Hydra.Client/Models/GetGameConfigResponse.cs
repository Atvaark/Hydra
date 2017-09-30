using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetGameConfigResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GameConfigLayer[] data { get; set; }
    }
}