using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetMessageChannelsByNameResponse : ServiceResult
    {
        [JsonProperty("data")]
        public MessageChannelsData[] data { get; set; }
    }
}
