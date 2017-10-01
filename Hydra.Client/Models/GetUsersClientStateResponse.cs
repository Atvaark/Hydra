using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUsersClientStateResponse : ServiceResult
    {
        [JsonProperty("data")]
        public UsersClientStateData[] data { get; set; }
    }
}
