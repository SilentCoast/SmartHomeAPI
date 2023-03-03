using Microsoft.AspNetCore.Mvc;
using SmartHomeAPI.Models;

namespace SmartHomeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MobileController : ControllerBase
    {
        private readonly SmartHomeContext db;
        public MobileController(SmartHomeContext db)
        {
            this.db = db;
        }
        [HttpPost]
        public void RegMobile(Mobile mobile)
        {
            db.Mobiles.Add(mobile);
        }
        [HttpGet]
        public List<Mobile> GetMobiles()
        {
            return (from p in db.Mobiles select p).ToList();
        }
        [HttpGet]
        public Mobile GetMobile()
        {
            return (from p in db.Mobiles select p).FirstOrDefault();
        }
    }
}
