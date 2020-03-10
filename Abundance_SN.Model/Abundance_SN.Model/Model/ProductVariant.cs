using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public string Key1 { get; set; }
        public string Value1 { get; set; }
        public string Key2 { get; set; }
        public string Value2 { get; set; }
        public string Key3 { get; set; }
        public string Value3 { get; set; }
    }
}
