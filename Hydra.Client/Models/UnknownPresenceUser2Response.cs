using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownPresenceUser2Response : ServiceResult
    {
        [JsonProperty("data")]
        public UnknownPresenceResponseData data { get; set; }
    }
}
