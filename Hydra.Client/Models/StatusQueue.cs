using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class StatusQueue
    {
        [JsonProperty("Party")]
        public PartyId Party { get; set; }

        [JsonProperty("PartyMembers")]
        public PartyMember[] PartyMembers { get; set; }

        [JsonProperty("MatchmakeTimer")]
        public int MatchmakeTimer { get; set; }

        [JsonProperty("TimeLeftRedyCheck")]
        public int? TimeLeftRedyCheck { get; set; }

        [JsonProperty("RematchMembers")]
        public GameSessionMember[] RematchMembers { get; set; }

        [JsonProperty("QueueType")]
        public int QueueType { get; set; }

        [JsonProperty("PlayList")]
        public string PlayList { get; set; }
    }
}