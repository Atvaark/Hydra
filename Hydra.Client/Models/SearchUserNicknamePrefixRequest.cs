using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class SearchUserNicknamePrefixRequest
    {
        [JsonProperty("nicknamePrefix")]
        public string nicknamePrefix { get; set; }

        [JsonProperty("maxResults")]
        public int maxResults { get; set; }
    }
}
