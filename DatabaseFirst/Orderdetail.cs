using System;
using System.Collections.Generic;

namespace DatabaseFirst
{
    public partial class Orderdetail
    {
        public int Id { get; set; }
        public int? Orderid { get; set; }
        public int? Itemid { get; set; }
        public int? Amount { get; set; }
        public double? Unitprice { get; set; }
        public double? Linetotal { get; set; }
    }
}
