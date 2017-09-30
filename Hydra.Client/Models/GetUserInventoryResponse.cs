using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUserInventoryResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UserInventoryData data { get; set; }
    }
}