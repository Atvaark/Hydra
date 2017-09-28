using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetEnvironmentsRequest
    {
        [JsonProperty("versions")]
        public HydraServiceVersion[] versions { get; set; }
    }
}