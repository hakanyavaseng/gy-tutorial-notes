﻿using System;
using System.Collections.Generic;

namespace DatabaseFirst.Entities
{
    public partial class Town
    {
        public int Id { get; set; }
        public short? Cityid { get; set; }
        public string? Town1 { get; set; }
    }
}
