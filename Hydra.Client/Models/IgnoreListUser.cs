using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class IgnoreListUser
    {
        [JsonProperty("User")]
        public UserId User { get; set; }

        [JsonProperty("ToIgnore")]
        public bool ToIgnore { get; set; }
    }
}