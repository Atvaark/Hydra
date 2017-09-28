using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class LoginResponse : BaseResponse
    {
        [JsonProperty("data")]
        public LoginResponseData data { get; set; }
    }
}