using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SteamGetCurrencyCodeRequest
    {
        [JsonProperty("steamId")]
        public string steamId { get; set; }
    }
}
