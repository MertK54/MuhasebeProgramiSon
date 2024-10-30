using System.Text.Json.Serialization;
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
                using(MySqlCommand command = new MySqlCommand("CALL sp_invoice_create_sale(@supplier_id,@customer_id,@stock_id,@product_name,@quantity,@unit_price,@total_amount,@type)",connection))
                {
                    command.Parameters.AddWithValue("supplier_id",createSaleInvoice.supplier_id);
                    command.Parameters.AddWithValue("stock_id",createSaleInvoice.stock_id);
                    command.Parameters.AddWithValue("customer_id",createSaleInvoice.customer_id);
                    command.Parameters.AddWithValue("product_name",createSaleInvoice.product_name);
                    command.Parameters.AddWithValue("quantity",createSaleInvoice.quantity);
                    command.Parameters.AddWithValue("unit_price",createSaleInvoice.unit_price);
                    command.Parameters.AddWithValue("total_amount",createSaleInvoice.total_amount);
                    command.Parameters.AddWithValue("type",createSaleInvoice.invoice_type);
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
                using(MySqlCommand command = new MySqlCommand("CALL sp_invoice_create_purchase(@supplier_id,@customer_id,@stock_id,@product_name,@quantity,@unit_price,@total_amount,@type)",connection))
                {
                    command.Parameters.AddWithValue("supplier_id",createSaleInvoice.supplier_id);
                    command.Parameters.AddWithValue("customer_id",createSaleInvoice.customer_id);
                    command.Parameters.AddWithValue("stock_id",createSaleInvoice.stock_id);
                    command.Parameters.AddWithValue("product_name",createSaleInvoice.product_name);
                    command.Parameters.AddWithValue("quantity",createSaleInvoice.quantity);
                    command.Parameters.AddWithValue("unit_price",createSaleInvoice.unit_price);
                    command.Parameters.AddWithValue("total_amount",createSaleInvoice.total_amount);
                    command.Parameters.AddWithValue("type",createSaleInvoice.invoice_type);
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
        [HttpPost]
        [Route("invoice-get-sale")]
        public IActionResult GetSaleInvoice([FromBody] GetSaleInvoiceType type)
        {
            var sale_invoice_list = new List<GetSaleInvoice>();
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_invoice_sale_get(@type)", connection))
                {
                    command.Parameters.AddWithValue("type", type.InvoiceType);
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var get_invoice_sale = new GetSaleInvoice()
                            {
                                invoice_id = reader["invoice_id"].ToString(),
                                supplier_id = reader["supplier_id"].ToString(),
                                customer_id = reader["customer_id"].ToString(),
                                stock_id = reader["stock_id"].ToString(),
                                product_name = reader["product_name"].ToString(),
                                quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                unit_price = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                                total_amount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                            };
                            sale_invoice_list.Add(get_invoice_sale);
                        }
                    }
                }
                connection.Close(); 
            }
            return Ok(sale_invoice_list); 

        }
        [HttpPost()]
        [Route("sale-return")]
        public IActionResult SaleReturnInvoice( [FromBody] UpdateStock updateStock)
        {
            string token = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_invoice_return(@stock_id,@quantity,@unit_price)",connection))
                {
                    command.Parameters.AddWithValue("@stock_id", updateStock.stock_id);
                    command.Parameters.AddWithValue("@quantity", updateStock.quantity);
                    command.Parameters.AddWithValue("@unit_price", updateStock.unit_price);
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            token = reader.GetString(0);
                        }
                    }
                }
            }
                    return Ok(new{token=token});
        }
        [HttpGet()]
        [Route("invoice-list-all")]
        public IActionResult AllListInvoice()
        {
            var allInvoice =new List<AllInvoice>();
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_invoice_all_get()",connection))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var get_invoice_all = new AllInvoice()
                            {
                                invoice_id = reader["invoice_id"].ToString(),
                                customer_id = reader["customer_id"].ToString(),
                                customer_name = reader["customer_name"].ToString(),
                                supplier_name = reader["supplier_name"].ToString(),
                                stock_id = reader["stock_id"].ToString(),
                                supplier_id = reader["supplier_id"].ToString(),
                                product_name = reader["product_name"].ToString(),
                                invoice_type = reader["type"].ToString(),
                                quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                unit_price = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                                total_amount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                            };
                            allInvoice.Add(get_invoice_all);
                        }
                    }
                }
            }
            return Ok(allInvoice);
        }

        [HttpDelete()]
        [Route("invoice-delete")]
        public IActionResult DeleteInvoice([FromBody]DeleteInvoice deleteInvoice)
        {
            string token = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_invoice_delete(@invoice_id)",connection))
                {
                    command.Parameters.AddWithValue("invoice_id", deleteInvoice.invoice_id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                        token = reader.GetString(0);
                        }
                    }
                }
            }
            return Ok( new {token = token});
        }
    }
}