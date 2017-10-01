using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class PlaylistStat
    {
        [JsonProperty("Playlist")]
        public string Playlist { get; set; }

        [JsonProperty("PlayersNumber")]
        public int PlayersNumber { get; set; }

        [JsonProperty("PlayersInQueue")]
        public int PlayersInQueue { get; set; }

        [JsonProperty("PlayersInGame")]
        public int PlayersInGame { get; set; }

        [JsonProperty("Sessions")]
        public int Sessions { get; set; }
    }
}