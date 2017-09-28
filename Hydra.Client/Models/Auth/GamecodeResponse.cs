using Newtonsoft.Json;

namespace Hydra.Client.Models.Auth
{
    public class GamecodeResponse
    {
        [JsonProperty("project")]
        public int project { get; set; }

        [JsonProperty("gamecode")]
        public string gamecode { get; set; }
    }
}