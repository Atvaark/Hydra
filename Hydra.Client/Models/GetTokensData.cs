using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetTokensData
    {
        [JsonProperty("AuthorizationToken")]
        public string AuthorizationToken { get; set; }

        [JsonProperty("DiagnosticToken")]
        public string DiagnosticToken { get; set; }

        [JsonProperty("AccessRoleToken")]
        public string AccessRoleToken { get; set; }

        [JsonProperty("ProviderToken")]
        public string ProviderToken { get; set; }

        [JsonProperty("Endpoints")]
        public Endpoint[] Endpoints { get; set; }

        [JsonProperty("Date")]
        public long Date { get; set; }
    }
}