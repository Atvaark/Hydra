using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SteamGetCurrencyCodeResponse : ServiceResult
    {
        [JsonProperty("data")]
        public string data { get; set; }
    }
}
