using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetPlaylistsStatsRequest
    {
        [JsonProperty("playlists")]
        public string[] playlists { get; set; }

        [JsonProperty("dataCenterIds")]
        public string[] dataCenterIds { get; set; }

        [JsonProperty("clientVersion")]
        public string clientVersion { get; set; }
    }
}
