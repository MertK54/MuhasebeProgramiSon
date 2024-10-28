namespace MuhasebeProgrami.Core
{
    public enum GetInvoiceType 
        {
            sale,
            purchase,
            sale_return,
            purchase_return
        }
        public class GetSaleInvoiceType
        {
        public InvoiceType invoice_type { get; set; }
        }
}