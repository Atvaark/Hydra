using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetDataCenterRequest
    {
        [JsonProperty("dataCenterId")]
        public string dataCenterId { get; set; }

        [JsonProperty("versions")]
        public HydraServiceVersion[] versions { get; set; }
    }
}
