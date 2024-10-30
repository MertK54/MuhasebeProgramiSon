namespace MuhasebeProgrami.Core
{
    public class AllInvoice
    {
        public string invoice_id { get; set;}
        public string customer_id { get; set; }
        public string stock_id { get; set; }
        public string supplier_id { get; set; }
        public string product_name { get; set; }
        public string customer_name { get; set; }
        public string supplier_name { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_amount { get; set; }
        public string invoice_type { get; set; }
    }
}