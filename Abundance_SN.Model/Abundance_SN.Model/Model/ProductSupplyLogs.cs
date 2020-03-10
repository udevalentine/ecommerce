using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class ProductSupplyLogs
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public System.DateTime SupplyDate { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public Product Product { get; set; }
        public ProductOrderLogs ProductOrderLogs { get; set; }
        public User User { get; set; }
        public Vendors Vendors { get; set; }
    }
}
