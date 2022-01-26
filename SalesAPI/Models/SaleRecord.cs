using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Models
{
    public class SaleRecord
    {
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public SaleRecord()
        {
        }

        public SaleRecord(int vendorId, int productId, DateTime date, decimal value)
        {
            VendorId = vendorId;
            ProductId = productId;
            Date = date;
            Value = value;
        }

        

    }
}
