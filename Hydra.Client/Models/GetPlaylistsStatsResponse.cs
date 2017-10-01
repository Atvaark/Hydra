using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetPlaylistsStatsResponse : ServiceResult
    {
        [JsonProperty("data")]
        public PlaylistData[] data { get; set; }
    }
}
