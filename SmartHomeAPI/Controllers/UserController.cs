using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SmartHomeAPI.Models;
using System.Net;
using System.Text;

namespace SmartHomeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SmartHomeContext db;
        public UserController(SmartHomeContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public bool RegisterUser([FromHeader]string Name, [FromHeader]string Login, [FromHeader] string Password)
        {
            //if Login already taken
            var searchedUser = (from p in db.Users where p.Login == Login select p).FirstOrDefault();
            if (searchedUser == null)
            {
                //Response.StatusCode = 208;
                byte[] data = Encoding.UTF8.GetBytes("Login taken");
                Response.Body.Write(data);
                return false;

            }
            
            User user = new User()
            {
                Name = Name,
                Password = Password,
                Login = Login
            };
            return true;
        }
    }
}
