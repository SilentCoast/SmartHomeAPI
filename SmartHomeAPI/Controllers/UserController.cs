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
        public int? RegisterUser([FromHeader]string Name, [FromHeader]string Login, [FromHeader] string Password)
        {
            //if Login already taken
            var searchedUser = (from p in db.Users where p.Login == Login select p).FirstOrDefault();
            if (searchedUser != null)
            {
                Response.StatusCode = 404;
                byte[] data = Encoding.UTF8.GetBytes("Login taken");
                Response.Body.WriteAsync(data);
                
                return null;
            }
            Setting setting= new Setting();
            db.Settings.Add(setting);
            User user = new User()
            {
                Name = Name,
                Password = Password,
                Login = Login,
                Setting= setting
            };
            db.Users.Add(user);
            db.SaveChanges();
            return user.Id;
        }
        [HttpGet("{Id}")]
        public User GetUser([FromRoute] int Id)
        {
            return (from p in db.Users where p.Id== Id select p).FirstOrDefault();
        }
        [HttpPost]
        public int? AuthorizeUser([FromHeader] string Login, [FromHeader]string Password)
        {
            var searched = (from p in db.Users where (p.Login==Login && p.Password==Password) select p).FirstOrDefault();
            if (searched == null)
            {
                Response.StatusCode = 500;
                return null;
            }
            return searched.Id;
        }
        

    }
}
