namespace MuhasebeProgrami.Core
{
    public enum InvoiceTypeGet 
        {
            sale,
            purchase,
            sale_return,
            purchase_return
        }
    public class GetSaleInvoice
    {
        public string invoice_id { get;set;}
        public string stock_id { get; set; }
        public string customer_id { get; set; }
        public string supplier_id { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_amount { get; set; }
        public InvoiceTypeGet invoice_type { get; set; }
    }
}
