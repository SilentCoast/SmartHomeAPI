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
        public int? RegMobile([FromHeader] string Uuid, [FromHeader] string AppId, [FromHeader] string DeviceName)
        {
            var searchedMobile = (from p in db.Mobiles where p.AppId== AppId where p.DeviceName==DeviceName select p).FirstOrDefault();
            if (searchedMobile != null)
            {
                searchedMobile.Uuid= Uuid;
                db.Mobiles.Update(searchedMobile);
                db.SaveChangesAsync();
                return searchedMobile.Id;
            }

            Mobile mobile = new Mobile()
            {
                Uuid= Uuid,
                AppId=AppId,
                DeviceName=DeviceName
            };
            db.Mobiles.Add(mobile);
            db.SaveChangesAsync();

            return mobile.Id;
        }
        [HttpGet]
        public List<Mobile> GetMobiles()
        {
            return (from p in db.Mobiles select p).ToList();
        }
        [HttpGet("{Id}")]
        public Mobile GetMobile([FromRoute]int Id)
        {
            return (from p in db.Mobiles where p.Id==Id select p).FirstOrDefault();
        }
    }
}
