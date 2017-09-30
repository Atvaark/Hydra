using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetEnvironmentsResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetEnvironmentsData data { get; set; }
    }
}