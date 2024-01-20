using System;
using System.Collections.Generic;

namespace DatabaseFirst
{
    public partial class City
    {
        public short Id { get; set; }
        public byte? Countryid { get; set; }
        public string? City1 { get; set; }
        public string? Region { get; set; }
    }
}
