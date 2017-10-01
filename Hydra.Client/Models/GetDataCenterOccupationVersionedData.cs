using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetDataCenterOccupationVersionedData
    {
        [JsonProperty("Versions")]
        public DataCenterOccupationVersions Versions { get; set; }

        [JsonProperty("UsersOnline")]
        public int UsersOnline { get; set; }

        [JsonProperty("DataCenterOccupation")]
        public DataCenterOccupation[] DataCenterOccupation { get; set; }
    }
}