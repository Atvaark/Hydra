using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetDataCenterOccupationVersionedRequest
    {
        [JsonProperty("state")]
        public int state { get; set; }

        [JsonProperty("generation")]
        public int generation { get; set; }

        [JsonProperty("versions")]
        public DataCenterOccupationVersions versions { get; set; }
    }
}
