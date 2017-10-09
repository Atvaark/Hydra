using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetProviderUsersRequest
    {
        [JsonProperty("providerId")]
        public string providerId { get; set; }

        [JsonProperty("providerUserIdList")]
        public string[] providerUserIdList { get; set; }
    }
}
