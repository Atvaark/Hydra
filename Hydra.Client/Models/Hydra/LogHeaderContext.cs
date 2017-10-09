using Newtonsoft.Json;

namespace Hydra.Client.Models.Hydra
{
    public class LogHeaderContext
    {
        [JsonProperty("PropertyName")]
        public string PropertyName { get; set; }

        [JsonProperty("PropertyValue")]
        public string PropertyValue { get; set; }
    }
}