using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
namespace MuhasebeProgramı.Controller
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Home()
        {
            return "";
        }
    }

}