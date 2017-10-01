using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TournamentPresence
    {
        [JsonProperty("ScheduledMatches")]
        public object[] ScheduledMatches { get; set; }

        [JsonProperty("Version")]
        public int Version { get; set; }
    }
}