using System.Data;
using Microsoft.AspNetCore.Mvc;
using MuhasebeProgrami.Core;
using MySql.Data.MySqlClient;

namespace MuhasebeProgrami.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly string _connectionString = "server=localhost;user=root;password=admin;database=creator_demo";
        [HttpPost()]
        [Route("customer-create")]
        public IActionResult CustomerCreate([FromBody] CreateCustomer customer)
        {
            string token = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_customer_create(@name,@e_mail,@phone_number,@adress)", connection))
                {
                    command.Parameters.AddWithValue("@name", customer.name);
                    command.Parameters.AddWithValue("@phone_number", customer.phone_number);
                    command.Parameters.AddWithValue("@e_mail", customer.e_mail);
                    command.Parameters.AddWithValue("@adress", customer.adress);
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            token = reader.GetString(0);
                        }
                    }
                }
                return Ok(new{token = token});
            }
        }
        [HttpGet()]
        [Route("customer-get")]
        public IActionResult CustomerGet()
        {
            var customers = new List<GetCustomers>(); 
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("CALL sp_customer_get()", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new GetCustomers() 
                            {
                                customer_id = reader["customer_id"].ToString(),
                                name = reader["name"].ToString(),
                                e_mail = reader["e_mail"].ToString(),
                                phone_number = reader["phone_number"].ToString(),
                                adress = reader["adress"].ToString(),
                            });
                        }
                    }
                    connection.Close(); 
                }
            }
            return Ok(customers); 
        }
        [HttpPost()]
        [Route("customer-update")]
        public IActionResult CustomerUpdate( [FromBody] UpdateCustomers updateCustomers)
        {
            string token = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("Call sp_customer_update(@customer_id,@name,@e_mail,@phone_number,@adress)",connection))
                {
                    command.Parameters.AddWithValue("@customer_id", updateCustomers.customer_id);
                    command.Parameters.AddWithValue("@name", updateCustomers.name);
                    command.Parameters.AddWithValue("@e_mail", updateCustomers.e_mail);
                    command.Parameters.AddWithValue("@phone_number", updateCustomers.phone_number);
                    command.Parameters.AddWithValue("@adress", updateCustomers.adress);
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            token = reader.GetString(0);
                        }
                    }
                }
            }
                return Ok(new{token = token});
        }
        [HttpDelete]
        [Route("customer-delete")]
        public IActionResult CustomerDelete([FromBody] CustomerDelete customer_id)
        {
            string token ="";
            using(MySqlConnection _connection = new MySqlConnection(_connectionString))
            {
                _connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_customer_delete(@customer_id)",_connection))
                {
                    command.Parameters.AddWithValue("@customer_id", customer_id.customer_id);
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
    public class CustomerDelete
    {
        public string customer_id { get; set; }
    }
}