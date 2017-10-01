using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetMessageChannelsByNameGsaResponse : ServiceResult
    {
        [JsonProperty("data")]
        public MessageChannelsData[] data { get; set; }
    }
}
