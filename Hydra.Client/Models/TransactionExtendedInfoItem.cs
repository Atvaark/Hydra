using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TransactionExtendedInfoItem
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}