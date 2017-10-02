using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class GetContainerResponse : ServiceResult
    {
        [JsonProperty("data")]
        public ContainerData data { get; set; }
    }
}
