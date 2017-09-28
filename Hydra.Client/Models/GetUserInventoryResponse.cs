using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUserInventoryResponse : BaseResponse
    {
        [JsonProperty("data")]
        public UserInventoryData data { get; set; }
    }
}