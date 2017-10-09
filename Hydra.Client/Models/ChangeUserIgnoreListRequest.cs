using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ChangeUserIgnoreListRequest
    {
        [JsonProperty("ignoreList")]
        public IgnoreListUser[] ignoreList { get; set; }
    }
}
