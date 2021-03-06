using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SquadInviteResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UnknownPresenceResponseData data { get; set; }
    }
}
