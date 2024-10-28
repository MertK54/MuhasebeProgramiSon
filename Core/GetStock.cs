namespace MuhasebeProgrami.Core
{
    public class GetStocks
    {
        public string stock_id { get; set; }
        public string supplier_name { get; set; }
        public string product_name { get; set;}
        public int quantity { get;set;}
        public Decimal unit_price { get; set;}
        public string supplier_id { get; set; }
    }
}