using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ServiceResult
    {
        [JsonProperty("retCode")]
        public int retCode { get; set; }
    }
}