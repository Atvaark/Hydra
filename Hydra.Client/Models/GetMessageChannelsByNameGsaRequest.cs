using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetMessageChannelsByNameGsaRequest
    {
        [JsonProperty("channelNames")]
        public string[] channelNames { get; set; }
    }
}
