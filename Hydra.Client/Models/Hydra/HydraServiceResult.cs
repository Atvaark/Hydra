using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class HydraServiceResult : HydraServiceData
    {
        [JsonProperty("retCode")]
        public int retCode { get; set; }
    }
}