using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class SendLogRequest
    {
        [JsonProperty("header")]
        public LogHeader header { get; set; }

        [JsonProperty("entries")]
        public int[] entries { get; set; }

        [JsonProperty("data")]
        public int[] data { get; set; }
    }
}
