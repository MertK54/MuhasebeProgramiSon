using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MuhasebeProgramı.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly string _connectionString = "server=localhost;user=root;password=admin;database=creator_demo";
        [HttpPost()]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest loginrequest)
        {
            string token = "";
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("CALL sp_login_user(@username,@password)", connection))
                {
                    command.Parameters.AddWithValue("@username", loginrequest.username);
                    command.Parameters.AddWithValue("@password", loginrequest.password);
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            token = reader.GetString(0);
                        }
                    }
                }
            }
                return Ok(new{token = token,redirectUrl = "/dashboard"});
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout([FromBody] LogoutRequest token)
        {
            string result = "";
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("CALL sp_logout_user(@token)",connection))
                {
                    command.Parameters.AddWithValue("@token",token.token);
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }  
            }
            return Ok(new { token = result });
        }

    public class LoginRequest
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }
    public class LogoutRequest
{
    public string? token { get; set; }
}
}
}
