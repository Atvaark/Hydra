using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class GetTransactionsRequest
    {
        [JsonProperty("fromTransactionId")]
        public int fromTransactionId { get; set; }
    }
}
