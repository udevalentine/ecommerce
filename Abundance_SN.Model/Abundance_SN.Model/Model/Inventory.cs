using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class Inventory
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public ProductVariantOptions ProductVariantOptions { get; set; } 
    }
}
