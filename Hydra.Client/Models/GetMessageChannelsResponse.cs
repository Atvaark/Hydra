using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetMessageChannelsResponse : ServiceResult
    {
        [JsonProperty("data")]
        public MessageChannelsData[] data { get; set; }
    }
}
