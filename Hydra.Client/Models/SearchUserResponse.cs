using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SearchUserResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UserId[] data { get; set; }
    }
}
