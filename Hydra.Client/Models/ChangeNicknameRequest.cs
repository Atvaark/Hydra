using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ChangeNicknameRequest
    {
        [JsonProperty("nickname")]
        public string nickname { get; set; }
    }
}
