using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetSessionsPagedResponse : ServiceResult
    {
        [JsonProperty("Sessions")]
        public GameSessionId[] Sessions { get; set; }

        [JsonProperty("ContinuationToken")]
        public string ContinuationToken { get; set; }
    }
}
