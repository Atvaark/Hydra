using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class StatusGame
    {
        [JsonProperty("GameSessionMembers")]
        public GameSessionMember[] GameSessionMembers { get; set; }

        [JsonProperty("GameType")]
        public int GameType { get; set; }
    }
}