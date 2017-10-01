using Newtonsoft.Json;

namespace Hydra.Client.Models.Auth
{
    public class VerifyResponse
    {
        [JsonProperty("oauth_token")]
        public string oauth_token { get; set; }

        [JsonProperty("session_id")]
        public string session_id { get; set; }

        [JsonProperty("entitlement_ids")]
        public int[] entitlement_ids { get; set; }

        [JsonProperty("beam_client_api_key")]
        public string beam_client_api_key { get; set; }

        [JsonProperty("token")]
        public string token { get; set; }

        [JsonProperty("beam_token")]
        public string[] beam_token { get; set; }
    }
}