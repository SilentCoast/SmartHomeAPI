﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmartHomeAPI.Models
{
    public partial class ThermostatDevice
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public double? Temperature { get; set; }
        public double? FanSpeed { get; set; }

        public virtual Room Room { get; set; }
    }
}