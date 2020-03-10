using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class CourierDetail
    {
        public int Id { get; set; }
        public CourierService CourierService { get; set; }
        public CourierAreaCharges CourierAreaCharges { get; set; }
        public State State { get; set; }
        public CourierCharges CourierCharges { get; set; }
        public decimal charges { get; set; }
    }
}
