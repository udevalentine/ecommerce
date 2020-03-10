using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class CustomerVisit
    {
        public long Id { get; set; }
        public string Uuid { get; set; }
        public DateTime? DateOfVisit { get; set; }

        public Product Product { get; set; }
    }
}
