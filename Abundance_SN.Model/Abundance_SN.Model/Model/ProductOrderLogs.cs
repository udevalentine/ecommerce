using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class ProductOrderLogs
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateRequested { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public Vendors Vendors { get; set; }
    }
}
