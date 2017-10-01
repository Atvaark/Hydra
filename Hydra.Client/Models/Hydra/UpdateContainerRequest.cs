using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class UpdateContainerRequest
    {
        [JsonProperty("containerName")]
        public string containerName { get; set; }

        [JsonProperty("data")]
        public ContainerRecord data { get; set; }
    }
}