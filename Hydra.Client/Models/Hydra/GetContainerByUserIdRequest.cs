using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class GetContainerByUserIdRequest
    {
        [JsonProperty("keys")]
        public string[] keys { get; set; }

        [JsonProperty("containerName")]
        public string containerName { get; set; }
    }
}
