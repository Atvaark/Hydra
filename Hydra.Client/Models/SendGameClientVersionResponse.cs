using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SendGameClientVersionResponse : ServiceResult
    {
        [JsonProperty("data")]
        public SendGameClientVersionData data { get; set; }
    }
}
