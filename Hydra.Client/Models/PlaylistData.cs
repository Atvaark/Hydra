using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class PlaylistData
    {
        [JsonProperty("DataCenterId")]
        public string DataCenterId { get; set; }

        [JsonProperty("PlaylistStats")]
        public PlaylistStat[] PlaylistStats { get; set; }
    }
}