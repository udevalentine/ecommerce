using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class SalesLogs
    {
        public long Id { get; set; }
        public string CartHash { get; set; }
        public int Quantity { get; set; }
        public int? Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public DateTime? DateOrdered { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public ProductVariantOptions ProductVariantOptions { get; set; }
        //public List<Product> Products { get; set; }
        //public List<ProductVariantOptions> ProductVariantOption { get; set; }
    }
}
