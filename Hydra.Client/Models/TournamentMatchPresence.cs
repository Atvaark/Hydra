using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TournamentMatchPresence
    {
        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("Status")]
        public object Status { get; set; }
    }
}