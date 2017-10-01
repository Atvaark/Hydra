using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UpdateClientContextResponse : ServiceResult
    {
        [JsonProperty("data")]
        public DedicatedServerData data { get; set; }
    }
}
