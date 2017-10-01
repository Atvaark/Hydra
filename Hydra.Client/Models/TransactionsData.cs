using Newtonsoft.Json;

namespace Hydra.Client.Models
{
    public class TransactionsData
    {
        [JsonProperty("Transactions")]
        public Transaction[] Transactions { get; set; }
    }
}