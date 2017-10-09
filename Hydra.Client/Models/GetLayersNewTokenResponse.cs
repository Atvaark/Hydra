using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetLayersNewTokenResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GameConfigLayer[] data { get; set; }
    }
}
