using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class MatchmakeByPlaylistResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UnknownPresenceResponseData data { get; set; }
    }
}
