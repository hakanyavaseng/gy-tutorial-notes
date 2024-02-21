using System;
using System.Collections.Generic;

namespace DatabaseFirst.Entities
{
    public partial class Invoicedetail
    {
        public int Id { get; set; }
        public int? Invoiceid { get; set; }
        public int? Orderdetailid { get; set; }
        public int? Itemid { get; set; }
        public int? Amount { get; set; }
        public double? Unitprice { get; set; }
        public double? Linetotal { get; set; }
    }
}
