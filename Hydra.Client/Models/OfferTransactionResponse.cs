using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class OfferTransactionResponse : ServiceResult
    {
        [JsonProperty("data")]
        public TransactionsData data { get; set; }
    }
}
