using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class GetContainerByNameRequest : HydraServiceData
    {
        [JsonProperty("containerName")]
        public string containerName { get; set; }
    }
}
