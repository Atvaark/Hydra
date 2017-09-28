using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class RefreshTokensToken
    {
        [JsonProperty("OldValue")]
        public string OldValue { get; set; }

        [JsonProperty("ProlongedValue")]
        public string ProlongedValue { get; set; }

        [JsonProperty("NewValue")]
        public string NewValue { get; set; }

        [JsonProperty("UpdatePolicy")]
        public int UpdatePolicy { get; set; }
    }
}