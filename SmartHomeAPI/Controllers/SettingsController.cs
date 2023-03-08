using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartHomeAPI.Models;

namespace SmartHomeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly SmartHomeContext db;
        public SettingsController(SmartHomeContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public Setting GetSetting([FromHeader] string token)
        {
            return LocalGetSetting(token);
        }
        [HttpPost]
        public int? UpdateSetting([FromHeader] string token, [FromHeader] string Username, [FromHeader] string Email, [FromHeader] string PhoneNumber, 
            [FromHeader] string Gender, [FromHeader] string DateOfBirth)
        {
            Setting setting = LocalGetSetting(token);
            setting.Username = Username;
            setting.Email = Email;
            setting.PhoneNumber = PhoneNumber;
            setting.Gender = Gender;
            setting.DateOfBirth = DateOfBirth;
            db.Settings.Update(setting); 
            db.SaveChanges();
            return setting.Id;
        }
        private Setting LocalGetSetting(string token)
        {
            var user = (from p in db.Users where p.Id == Convert.ToInt32(token) select p).First();
            var setting = (from p in db.Settings where p.Id == user.SettingId select p).FirstOrDefault();
            if (setting != null)
            {
                return setting;
            }
            //if user does not have any settings - create one
            Setting newsetting = new Setting();

            db.Settings.Add(newsetting);
            db.SaveChanges();
            user.SettingId = newsetting.Id;
            db.Users.Update(user);
            db.SaveChanges();
            
            return newsetting;
        }
    }
}
