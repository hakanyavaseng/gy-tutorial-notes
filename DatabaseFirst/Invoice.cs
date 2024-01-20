using System;
using System.Collections.Generic;

namespace DatabaseFirst
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public int? Orderid { get; set; }
        public DateTime? Date { get; set; }
        public int? Addressid { get; set; }
        public string? Cargoficheno { get; set; }
        public double? Totalprice { get; set; }
    }
}
