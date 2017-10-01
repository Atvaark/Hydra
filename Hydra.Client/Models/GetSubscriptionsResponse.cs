using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetSubscriptionsResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetSubscriptionsData[] data { get; set; }
    }
}
