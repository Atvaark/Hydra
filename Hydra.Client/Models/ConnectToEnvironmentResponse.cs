using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class ConnectToEnvironmentResponse : ServiceResult
    {
        [JsonProperty("data")]
        public GetEnvironmentsData data { get; set; }
    }
}