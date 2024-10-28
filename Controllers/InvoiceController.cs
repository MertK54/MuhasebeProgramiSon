using Microsoft.AspNetCore.Mvc;
using MuhasebeProgrami.Core;
using MySql.Data.MySqlClient;

namespace MuhasebeProgrami.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController:ControllerBase
    {
        private readonly string _connectionString = "server=localhost;user=root;password=admin;database=creator_demo";
        [HttpPost()]
        [Route("invoice-create-sale")]
        public IActionResult CreateSaleInvoice([FromBody]CreateSaleInvoice createSaleInvoice)
        {
            string token ="";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_invoice_create_sale(@supplier_id,@customer_id,@product_name,@quantity,@unit_price,@type)",connection))
                {
                    command.Parameters.AddWithValue("supplier_id",createSaleInvoice.supplier_id);
                    command.Parameters.AddWithValue("customer_id",createSaleInvoice.customer_id);
                    command.Parameters.AddWithValue("product_name",createSaleInvoice.product_name);
                    command.Parameters.AddWithValue("quantity",createSaleInvoice.quantity);
                    command.Parameters.AddWithValue("unit_price",createSaleInvoice.unit_price);
                    command.Parameters.AddWithValue("type",createSaleInvoice.invoice_type.ToString());
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {   
                        while(reader.Read())
                        {
                            token = reader.GetString(0);
                        }
                    }
                }
                connection.Close(); 
            }
            return Ok(new{token=token});
        }
        [HttpPost()]
        [Route("invoice-create-purchase")]
        public IActionResult CreatePurchaseInvoice([FromBody]CreateSaleInvoice createSaleInvoice)
        {
            string token ="";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_invoice_create_purchase(@supplier_id,@customer_id,@product_name,@quantity,@unit_price,@type)",connection))
                {
                    command.Parameters.AddWithValue("supplier_id",createSaleInvoice.supplier_id);
                    command.Parameters.AddWithValue("customer_id",createSaleInvoice.customer_id);
                    command.Parameters.AddWithValue("product_name",createSaleInvoice.product_name);
                    command.Parameters.AddWithValue("quantity",createSaleInvoice.quantity);
                    command.Parameters.AddWithValue("unit_price",createSaleInvoice.unit_price);
                    command.Parameters.AddWithValue("type",createSaleInvoice.invoice_type.ToString());
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {   
                        while(reader.Read())
                        {
                            token = reader.GetString(0);
                        }
                    }
                }
                connection.Close(); 
            }
            return Ok(new{token=token});
        }
    }
}