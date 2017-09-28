using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class LoginClientData
    {
        [JsonProperty("BuildConfiguration")]
        public string BuildConfiguration { get; set; }

        [JsonProperty("BuildVersion")]
        public string BuildVersion { get; set; }

        [JsonProperty("ProductBranch")]
        public string ProductBranch { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("RequestedProfession")]
        public int RequestedProfession { get; set; }

        [JsonProperty("LauncherType")]
        public string LauncherType { get; set; }
    }
}