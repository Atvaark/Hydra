using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class RefreshTokensData
    {
        [JsonProperty("Tokens")]
        public RefreshTokensToken[] Tokens { get; set; }

        [JsonProperty("ProviderToken")]
        public string ProviderToken { get; set; }
    }
}