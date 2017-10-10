using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUserIgnoreListResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UserId[] data { get; set; }
    }
}
