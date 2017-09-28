using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class BaseResponse
    {
        [JsonProperty("retCode")]
        public int retCode { get; set; }
    }
}