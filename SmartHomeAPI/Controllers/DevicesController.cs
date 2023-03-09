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
        public List<DeviceDTO> GetDevices([FromRoute] int roomId)
        {
            List<DeviceDTO> devices= new List<DeviceDTO>();
            var g = (from p in db.Devices where p.RoomId==roomId select p).ToList();
            foreach (var device in g)
            {
                DeviceDTO deviceDTO = new DeviceDTO()
                {
                    Id = device.Id,
                    Type = device.Type,
                    
                    FanSpeed = 0,
                    Temperature = 0,
                    LightBrightness = 0,
                    IsActive = false
                };
                if (device.FanSpeed.HasValue)
                {
                    deviceDTO.FanSpeed = (int)device.FanSpeed;
                }
                if(device.Temperature.HasValue)
                {
                    deviceDTO.Temperature = (int)device.Temperature;
                }
                if (device.LightBrightness.HasValue)
                {
                    deviceDTO.LightBrightness = (int)device.LightBrightness;
                }
                if(device.IsActive.HasValue)
                {
                    deviceDTO.IsActive = (bool)device.IsActive;
                }
                devices.Add(deviceDTO);
            }
            return devices;
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
        public bool UpdateDevice([FromHeader] string Type, [FromHeader] int Id, [FromHeader] int? LightBrightness = null
            ,[FromHeader] int? Temperature = null, [FromHeader] int? FanSpeed = null, [FromHeader] bool IsActive = false)
        {
            var device = (from p in db.Devices where p.Id == Id select p).First();

           
            device.LightBrightness = LightBrightness;
            device.Temperature = Temperature;
            device.FanSpeed = FanSpeed;
            device.IsActive = IsActive;
            db.Devices.Update(device);
            db.SaveChanges();
            return true;
        }
    }
}
