using Newtonsoft.Json;

namespace Hydra.Client.Models.Unknown
{
    public class UnknownPresenceResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UnknownPresenceResponseData data { get; set; }
    }

    public class UnknownPresenceResponseData
    {
        [JsonProperty("User")]
        public object User { get; set; }

        [JsonProperty("Squad")]
        public Squad Squad { get; set; }

        [JsonProperty("Matchmake")]
        public Matchmake Matchmake { get; set; }

        [JsonProperty("TournamentMatch")]
        public TournamentMatch TournamentMatch { get; set; }

        [JsonProperty("Tournament")]
        public Tournament Tournament { get; set; }
    }

    public class TournamentMatch
    {
        [JsonProperty("Version")]
        public int Version { get; set; }
        [JsonProperty("Status")]
        public object Status { get; set; }
    }


    public class Matchmake
    {
        [JsonProperty("Version")]
        public long Version { get; set; }

        [JsonProperty("UserEvents")]
        public object[] UserEvents { get; set; }

        [JsonProperty("Penalty")]
        public int Penalty { get; set; }

        [JsonProperty("Status")]
        public object Status { get; set; }
    }


    public class Squad
    {
        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("Status")]
        public object Status { get; set; }

        [JsonProperty("InviteEvents")]
        public object[] InviteEvents { get; set; }

        [JsonProperty("SquadEvents")]
        public object[] SquadEvents { get; set; }
    }
    
    public class Tournament
    {
        [JsonProperty("ScheduledMatches")]
        public object[] ScheduledMatches { get; set; }

        [JsonProperty("Version")]
        public int Version { get; set; }
    }
}
