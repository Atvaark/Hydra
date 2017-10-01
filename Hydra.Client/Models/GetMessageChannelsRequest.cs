using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetMessageChannelsRequest
    {
        [JsonProperty("channels")]
        public GetMessageChannel2[] channels { get; set; }
    }
}
