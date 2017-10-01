using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class Message
    {
        [JsonProperty("From")]
        public UserId From { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("Type")]
        public int Type { get; set; }
    }
}