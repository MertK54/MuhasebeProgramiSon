using System.Data;
using Microsoft.AspNetCore.Mvc;
using MuhasebeProgrami.Core;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace MuhasebeProgrami.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly string _connectionString = "server=localhost;user=root;password=admin;database=creator_demo";
        [HttpPost()]
        [Route("supplier-create")]
        public ActionResult CreateSupplier([FromBody] CreateSupplier supplier)
        {
            string token="";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_supplier_create(@name,@e_mail,@adress,@phone_number)", connection))
                {
                    command.Parameters.AddWithValue("@name",supplier.name);
                    command.Parameters.AddWithValue("@e_mail",supplier.e_mail);
                    command.Parameters.AddWithValue("@adress",supplier.adress);
                    command.Parameters.AddWithValue("@phone_number",supplier.phone_number);
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
        [Route("supplier-get")]
        public IActionResult GetSupplier()
        {
            var supplierList = new List<GetSupplier>();
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_supplier_get()",connection))
                {
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var get_supplier = new GetSupplier() 
                            {
                                supplier_id = reader["supplier_id"].ToString(),
                                name = reader["name"].ToString(),
                                e_mail = reader["e_mail"].ToString(),
                                phone_number = reader["phone_number"].ToString(),
                                adress = reader["adress"].ToString(),
                                created_at = reader["created_at"].ToString(),
                            };
                            supplierList.Add(get_supplier);
                        }
                    }
                }
            }
            return Ok(supplierList);
        }
        [HttpPost()]
        [Route("supplier-update")]
        public IActionResult SupplierUpdate( [FromBody] UpdateSupplier updateSupplier)
        {
            string token = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_supplier_update(@supplier_id,@name,@phone_number,@e_mail,@adress)",connection))
                {
                    command.Parameters.AddWithValue("@supplier_id", updateSupplier.supplier_id);
                    command.Parameters.AddWithValue("@name", updateSupplier.name);
                    command.Parameters.AddWithValue("@e_mail", updateSupplier.e_mail);
                    command.Parameters.AddWithValue("@phone_number", updateSupplier.phone_number);
                    command.Parameters.AddWithValue("@adress", updateSupplier.adress);
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
        [HttpDelete()]
        [Route("supplier-delete")]
        public IActionResult CustomerDelete([FromBody] DeleteSupplier supplier_id)
        {
            string result = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(MySqlCommand command = new MySqlCommand("CALL sp_supplier_delete(@supplier_id)",connection))
                {
                    command.Parameters.AddWithValue("@supplier_id",supplier_id.supplier_id);
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            return Ok(result);
        }
    }
}