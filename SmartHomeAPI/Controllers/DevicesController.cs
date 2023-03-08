using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SmartHomeAPI.Models;

namespace SmartHomeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly SmartHomeContext db;
        public DevicesController(SmartHomeContext db)
        {
            this.db = db;
        }
        [HttpGet("{roomId}")]
        public List<Device> GetDevices([FromRoute] int roomId)
        {
            return (from p in db.Devices where p.RoomId==roomId select p).ToList();
        }
        [HttpPost]
        public int? AddDevice([FromHeader] int roomId, [FromHeader] string Type)
        {
            Device device = new Device()
            {
                Type = Type,
                RoomId = roomId
                
            };
            db.Devices.Add(device);
            db.SaveChanges();
            return device.Id;
        }
        [HttpPatch]
        public bool UpdateDevice([FromHeader] string Type, [FromHeader] int Id, [FromHeader] string? LightBrightness = null
            ,[FromHeader] string? Temperature = null, [FromHeader] string? FanSpeed = null, [FromHeader] bool IsActive = false)
        {
            var device = (from p in db.Devices where p.Id == Id select p).First();

            device.LightBrightness = Convert.ToDouble(LightBrightness);
            device.Temperature = Convert.ToDouble(Temperature);
            device.FanSpeed = Convert.ToDouble(FanSpeed);
            device.IsActive = IsActive;
            db.Devices.Update(device);
            db.SaveChanges();
            return true;
        }
    }
}
