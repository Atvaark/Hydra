using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetMessageChannelsByNameRequest
    {
        [JsonProperty("channelNames")]
        public string[] channelNames { get; set; }
    }
}
