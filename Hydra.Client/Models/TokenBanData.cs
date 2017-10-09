using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TokenBanData
    {
        [JsonProperty("Token")]
        public LoginToken Token { get; set; }

        [JsonProperty("BanReason")]
        public int BanReason { get; set; }

        [JsonProperty("BanStart")]
        public int BanStart { get; set; }

        [JsonProperty("BanEnd")]
        public int BanEnd { get; set; }
    }
}