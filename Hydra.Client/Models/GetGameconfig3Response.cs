using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetGameconfig3Response : ServiceResult
    {
        [JsonProperty("data")]
        public string data { get; set; }
    }
}
