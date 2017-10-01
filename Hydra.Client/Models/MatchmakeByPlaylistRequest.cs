using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class MatchmakeByPlaylistRequest
    {
        [JsonProperty("playlist")]
        public string playlist { get; set; }

        [JsonProperty("dcPings")]
        public DataCenterPing[] dcPings { get; set; }
    }
}
