using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetTransactionsResponse : ServiceResult
    {
        [JsonProperty("data")]
        public TransactionsData data { get; set; }
    }
}
