using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ProviderUser
    {
        [JsonProperty("ProviderId")]
        public string ProviderId { get; set; }

        [JsonProperty("ProviderUserId")]
        public string ProviderUserId { get; set; }

        [JsonProperty("BaseData")]
        public GetUsersData BaseData { get; set; }
    }
}