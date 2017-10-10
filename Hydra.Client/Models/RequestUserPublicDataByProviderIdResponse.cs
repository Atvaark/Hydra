using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class RequestUserPublicDataByProviderIdResponse : ServiceResult
    {
        [JsonProperty("data")]
        public ProviderUser[] data { get; set; }
    }
}
