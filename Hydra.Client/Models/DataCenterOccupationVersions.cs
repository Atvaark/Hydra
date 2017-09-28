using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class DataCenterOccupationVersions
    {
        [JsonProperty("Versions")]
        public int[] Versions { get; set; }
    }
}