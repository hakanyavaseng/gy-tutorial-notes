using System;
using System.Collections.Generic;

namespace DatabaseFirst
{
    public partial class Address
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public byte? Countryid { get; set; }
        public short? Cityid { get; set; }
        public int? Townid { get; set; }
        public int? Districtid { get; set; }
        public string? Postalcode { get; set; }
        public string? Addresstext { get; set; }
    }
}
