using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class UpdateUserIgnoreListRequest
    {
        [JsonProperty("ignoreList")]
        public IgnoreListUser[] ignoreList { get; set; }
    }
}
