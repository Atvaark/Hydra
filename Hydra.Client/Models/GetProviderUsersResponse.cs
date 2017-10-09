using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetProviderUsersResponse : ServiceResult
    {
        [JsonProperty("data")]
        public ProviderUser[] data { get; set; }
    }
}
