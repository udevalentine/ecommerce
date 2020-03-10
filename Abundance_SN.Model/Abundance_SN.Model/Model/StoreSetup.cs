using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class StoreSetup
    {
        public long Id { get; set; }
        public string ShopName { get; set; }
        public string StoreProductType { get; set; }
        public string BusinessName { get; set; }
        public string BusinessRcNo { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public string StoreLogoUrl { get; set; }
        public string AboutUs { get; set; }
        public string StoreAddress { get; set; }

        public Person Person { get; set; }
    }
}
