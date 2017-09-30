using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUsersResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetUsersData[] data { get; set; }
    }
}