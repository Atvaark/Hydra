using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class MatchmakePresence
    {
        [JsonProperty("Version")]
        public long Version { get; set; }

        [JsonProperty("UserEvents")]
        public object[] UserEvents { get; set; }

        [JsonProperty("Penalty")]
        public int Penalty { get; set; }

        [JsonProperty("Status")]
        public MatchmakeStatus Status { get; set; }
    }
}