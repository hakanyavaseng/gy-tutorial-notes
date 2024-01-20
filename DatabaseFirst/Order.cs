using System;
using System.Collections.Generic;

namespace DatabaseFirst
{
    public partial class Order
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public DateTime? Date { get; set; }
        public double? Totalprice { get; set; }
        public byte? Status { get; set; }
        public int? Addressid { get; set; }
    }
}
