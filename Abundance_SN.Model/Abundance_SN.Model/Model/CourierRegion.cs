using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class CourierRegion
    {
        public int Region_Id { get; set; }
        public string Region_Name { get; set; }
        public CourierService CourierService { get; set; }
    }
}
