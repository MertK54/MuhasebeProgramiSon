using Microsoft.AspNetCore.Mvc;
using MuhasebeProgrami.Core;
using MySql.Data.MySqlClient;
namespace MuhasebeProgrami.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly string _connectionString = "server=localhost;user=root;password=admin;database=creator_demo";
        [HttpPost()]
        [Route("stock-create")]
        public ActionResult CreateStock([FromBody] CreateStock stock)
        {
            string token="";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_stock_create(@product_name,@quantity,@unit_price,@supplier_id)", connection))
                {
                    command.Parameters.AddWithValue("@product_name",stock.product_name);
                    command.Parameters.AddWithValue("@quantity",stock.quantity);
                    command.Parameters.AddWithValue("@unit_price",stock.unit_price);
                    command.Parameters.AddWithValue("@supplier_id",stock.supplier_id);
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
        [Route("stock-get")]
        public IActionResult GetStock()
        {
            var stockList = new List<GetStocks>();
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_stock_get()",connection))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var get_stock = new GetStocks() 
                            {
                                stock_id = reader["stock_id"].ToString(),
                                supplier_id = reader["supplier_id"].ToString(),
                                supplier_name = reader["supplier_name"].ToString(),
                                product_name = reader["product_name"].ToString(),
                                quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                unit_price = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                            };
                            stockList.Add(get_stock);
                        }
                    }
                }
                connection.Close(); 
            }
            return Ok(stockList);
        }
        [HttpPost()]
        [Route("stock-update-reduce")]
        public IActionResult StockUpdate( [FromBody] UpdateStock updateStock)
        {
            string token = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_stock_update_reduce(@stock_id,@quantity,@unit_price)",connection))
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

        [HttpPost()]
        [Route("stock-update-add")]
        public IActionResult StockUpdateAdd( [FromBody] UpdateStock updateStock)
        {
            string token = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_stock_update_add(@stock_id,@quantity,@unit_price)",connection))
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
        
        [HttpDelete()]
        [Route("stock-delete")]
        public IActionResult StockDelete([FromBody] DeleteStock stock_id)
        {
            string token = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_stock_delete(@stock_id)",connection))
                {
                    command.Parameters.AddWithValue("@stock_id",stock_id.stock_id);
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
    }
}