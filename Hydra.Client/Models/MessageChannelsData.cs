using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class MessageChannelsData
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Version")]
        public int Version { get; set; }

        [JsonProperty("LastReadVersion")]
        public int LastReadVersion { get; set; }

        [JsonProperty("IsPersistent")]
        public bool IsPersistent { get; set; }

        [JsonProperty("Messages")]
        public Message[] Messages { get; set; }
    }
}