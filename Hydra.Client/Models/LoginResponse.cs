using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class LoginResponse : ServiceResult
    {
        [JsonProperty("data")]
        public LoginResponseData data { get; set; }
    }
}