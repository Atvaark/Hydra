using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetEnvironmentListRequest
    {
        [JsonProperty("versions")]
        public HydraServiceVersion[] versions { get; set; }
    }
}