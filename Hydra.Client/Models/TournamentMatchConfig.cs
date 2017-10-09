using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TournamentMatchConfig
    {
        [JsonProperty("GameMode")]
        public int GameMode { get; set; }

        [JsonProperty("GameMap")]
        public string GameMap { get; set; }

        [JsonProperty("TimeLimit")]
        public int TimeLimit { get; set; }

        [JsonProperty("FragLimit")]
        public int FragLimit { get; set; }
    }
}