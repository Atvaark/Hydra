using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUserSubscriptionResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetSubscriptionsData[] data { get; set; }
    }
}
