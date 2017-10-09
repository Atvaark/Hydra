using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UnknownUserService1Response : ServiceResult
    {
        [JsonProperty("data")]
        public UserId[] data { get; set; }
    }
}
