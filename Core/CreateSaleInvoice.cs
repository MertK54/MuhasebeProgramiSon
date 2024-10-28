namespace MuhasebeProgrami.Core
{
    public enum InvoiceType 
        {
            sale,
            purchase,
            sale_return,
            purchase_return
        }
    public class CreateSaleInvoice
    {
        public string customer_id { get; set; }
        public string supplier_id { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_amount { get; set; }
        public InvoiceType invoice_type { get; set; }
    }
}
