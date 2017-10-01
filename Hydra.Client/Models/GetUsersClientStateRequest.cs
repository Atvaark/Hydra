using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetUsersClientStateRequest
    {
        [JsonProperty("users")]
        public UserId[] users { get; set; }
    }
}
