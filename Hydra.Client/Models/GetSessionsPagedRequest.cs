using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetSessionsPagedRequest
    {
        [JsonProperty("pageSize")]
        public int pageSize { get; set; }

        [JsonProperty("continuationToken")]
        public string continuationToken { get; set; }
    }
}
