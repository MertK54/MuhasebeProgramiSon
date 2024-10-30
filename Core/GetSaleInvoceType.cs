using System.Text.Json.Serialization;

namespace MuhasebeProgrami.Core
{
    public class GetSaleInvoiceType
    {
        [JsonPropertyName("type")]
        public string InvoiceType { get; set; } 
    }

}