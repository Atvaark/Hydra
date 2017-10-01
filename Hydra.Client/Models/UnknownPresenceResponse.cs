using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownPresenceResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UnknownPresenceResponseData data { get; set; }
    }
}
