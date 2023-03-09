using System;

namespace SmartHomeAPI.Models
{
    public class DeviceDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int FanSpeed { get; set; }
        public int Temperature { get; set; }
        public int LightBrightness { get; set; }
        public bool IsActive { get; set; }
    }
}
