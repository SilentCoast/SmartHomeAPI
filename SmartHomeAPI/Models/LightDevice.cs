﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmartHomeAPI.Models
{
    public partial class LightDevice
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int? Brightness { get; set; }

        public virtual Room Room { get; set; }
    }
}