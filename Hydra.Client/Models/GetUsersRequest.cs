using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUsersRequest
    {
        [JsonProperty("users")]
        public UserId[] users { get; set; }
    }
}