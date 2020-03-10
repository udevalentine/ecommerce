using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
     public  class ProductVariantOptions
    {
        public long Id { get; set; }
        public string ProductOptionName { get; set; }

        public ProductVariant ProductVariant { get; set; }
        public decimal? Price { get; set; }
        public string ImageUrl { get; set; }
        public int VariantQuantity { get; set; }
    }
}
