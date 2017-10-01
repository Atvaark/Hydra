using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class MatchmakeStatus
    {
        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("GameSession")]
        public GameSessionId GameSession { get; set; }

        [JsonProperty("RematchSession")]
        public GameSessionId RematchSession { get; set; }

        [JsonProperty("GameSessionContext")]
        public string GameSessionContext { get; set; }

        [JsonProperty("StatusQueue")]
        public StatusQueue StatusQueue { get; set; }

        [JsonProperty("StatusGame")]
        public StatusGame StatusGame { get; set; }

        [JsonProperty("PlayList")]
        public string PlayList { get; set; }
    }
}