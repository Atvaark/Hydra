using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class GetContainerByNameRequest
    {
        [JsonProperty("containerName")]
        public string containerName { get; set; }
    }
}
