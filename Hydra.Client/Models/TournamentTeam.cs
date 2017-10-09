using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TournamentTeam
    {
        [JsonProperty("TeamDesc")]
        public string TeamDesc { get; set; }

        [JsonProperty("Color")]
        public int Color { get; set; }
    }
}