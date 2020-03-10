using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class ProductImageGallary
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool Activated { get; set; }
        public  Product Product { get; set; }
    }
}
